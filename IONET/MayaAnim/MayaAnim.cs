using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO;
using IONET.Core.Skeleton;
using IONET.Core.Animation;

namespace IONET.MayaAnim
{
    public class MayaAnim
    {
        private enum InfinityType
        {
            constant,
            linear,
            cycle,
            cycleRelative,
            oscillate
        }

        private enum InputType
        {
            time,
            unitless
        }

        private enum OutputType
        {
            time,
            linear,
            angular,
            unitless
        }

        private enum ControlType
        {
            translate,
            rotate,
            scale,
            visibility
        }

        private enum TrackType
        {
            translateX,
            translateY,
            translateZ,
            rotateX,
            rotateY,
            rotateZ,
            scaleX,
            scaleY,
            scaleZ,
            visibility
        }

        private class Header
        {
            public float animVersion;
            public string mayaVersion;
            public float startTime;
            public float endTime;
            public float startUnitless;
            public float endUnitless;
            public string timeUnit;
            public string linearUnit;
            public string angularUnit;

            public Header()
            {
                animVersion = 1.1f;
                mayaVersion = "2015";
                startTime = 1;
                endTime = 1;
                startUnitless = 0;
                endUnitless = 0;
                timeUnit = "ntscf";
                linearUnit = "cm";
                angularUnit = "rad";
            }
        }

        private class AnimKey
        {
            public float input, output;
            public string intan, outtan;
            public float t1 = 0, w1 = 1;
            public float t2 = 0, w2 = 1;

            public AnimKey()
            {
                intan = "linear";
                outtan = "linear";
            }
        }

        private class AnimData
        {
            public ControlType controlType;
            public TrackType type;
            public InputType input;
            public OutputType output;
            public InfinityType preInfinity, postInfinity;
            public bool weighted = false;
            public List<AnimKey> keys = new List<AnimKey>();

            public AnimData()
            {
                input = InputType.time;
                output = OutputType.linear;
                preInfinity = InfinityType.constant;
                postInfinity = InfinityType.constant;
                weighted = false;
            }
        }

        private class AnimBone
        {
            public string name;
            public List<AnimData> atts = new List<AnimData>();
        }

        private Header header;
        private List<AnimBone> Bones = new List<AnimBone>();

        public string Name = "Animation";

        public MayaAnim()
        {
            header = new Header();
        }

        public void Open(string fileName)
        {
            Name = Path.GetFileNameWithoutExtension(fileName);
            using (StreamReader r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                AnimData currentData = null;
                while (!r.EndOfStream)
                {
                    var line = r.ReadLine();
                    var args = line.Trim().Replace(";", "").Split(' ');

                    switch (args[0])
                    {
                        case "animVersion":
                            header.animVersion = float.Parse(args[1]);
                            break;
                        case "mayaVersion":
                            header.mayaVersion = args[1];
                            break;
                        case "timeUnit":
                            header.timeUnit = args[1];
                            break;
                        case "linearUnit":
                            header.linearUnit = args[1];
                            break;
                        case "angularUnit":
                            header.angularUnit = args[1];
                            break;
                        case "startTime":
                            header.startTime = float.Parse(args[1]);
                            break;
                        case "endTime":
                            header.endTime = float.Parse(args[1]);
                            break;
                        case "anim":
                            if (args.Length != 7)
                                continue;
                            var currentNode = Bones.Find(e => e.name.Equals(args[3]));
                            if (currentNode == null)
                            {
                                currentNode = new AnimBone();
                                currentNode.name = args[3];
                                Bones.Add(currentNode);
                            }
                            currentData = new AnimData();
                            currentData.controlType = (ControlType)Enum.Parse(typeof(ControlType), args[1].Split('.')[0]);
                            currentData.type = (TrackType)Enum.Parse(typeof(TrackType), args[2]);
                            currentNode.atts.Add(currentData);
                            break;
                        case "animData":
                            if (currentData == null)
                                continue;
                            string dataLine = r.ReadLine();
                            while (!dataLine.Contains("}"))
                            {
                                var dataArgs = dataLine.Trim().Replace(";", "").Split(' ');
                                switch (dataArgs[0])
                                {
                                    case "input":
                                        currentData.input = (InputType)Enum.Parse(typeof(InputType), dataArgs[1]);
                                        break;
                                    case "output":
                                        currentData.output = (OutputType)Enum.Parse(typeof(OutputType), dataArgs[1]);
                                        break;
                                    case "weighted":
                                        currentData.weighted = dataArgs[1] == "1";
                                        break;
                                    case "preInfinity":
                                        currentData.preInfinity = (InfinityType)Enum.Parse(typeof(InfinityType), dataArgs[1]);
                                        break;
                                    case "postInfinity":
                                        currentData.postInfinity = (InfinityType)Enum.Parse(typeof(InfinityType), dataArgs[1]);
                                        break;
                                    case "keys":
                                        string keyLine = r.ReadLine();
                                        while (!keyLine.Contains("}"))
                                        {
                                            var keyArgs = keyLine.Trim().Replace(";", "").Split(' ');

                                            var key = new AnimKey()
                                            {
                                                input = float.Parse(keyArgs[0]),
                                                output = float.Parse(keyArgs[1])
                                            };

                                            if (keyArgs.Length >= 7)
                                            {
                                                key.intan = keyArgs[2];
                                                key.outtan = keyArgs[3];
                                            }

                                            if (key.intan == "fixed")
                                            {
                                                key.t1 = float.Parse(keyArgs[7]);
                                                key.w1 = float.Parse(keyArgs[8]);
                                            }
                                            if (key.outtan == "fixed" && keyArgs.Length > 9)
                                            {
                                                key.t2 = float.Parse(keyArgs[9]);
                                                key.w2 = float.Parse(keyArgs[10]);
                                            }

                                            currentData.keys.Add(key);

                                            keyLine = r.ReadLine();
                                        }
                                        break;

                                }
                                dataLine = r.ReadLine();
                            }
                            break;
                    }
                }
            }
        }

        public void Save(string fileName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                file.WriteLine("animVersion " + header.animVersion + ";");
                file.WriteLine("mayaVersion " + header.mayaVersion + ";");
                file.WriteLine("timeUnit " + header.timeUnit + ";");
                file.WriteLine("linearUnit " + header.linearUnit + ";");
                file.WriteLine("angularUnit " + header.angularUnit + ";");
                file.WriteLine("startTime " + 1 + ";");
                file.WriteLine("endTime " + header.endTime + ";");

                int Row = 0;

                foreach (AnimBone animBone in Bones)
                {
                    int TrackIndex = 0;
                    if (animBone.atts.Count == 0)
                    {
                        file.WriteLine($"anim {animBone.name} 0 1 {TrackIndex++};");
                    }
                    foreach (AnimData animData in animBone.atts)
                    {
                        file.WriteLine($"anim {animData.controlType}.{animData.type} {animData.type} {animBone.name} 0 1 {TrackIndex++};");
                        file.WriteLine("animData {");
                        file.WriteLine($" input {animData.input};");
                        file.WriteLine($" output {animData.output};");
                        file.WriteLine($" weighted {(animData.weighted ? 1 : 0)};");
                        file.WriteLine($" preInfinity {animData.preInfinity};");
                        file.WriteLine($" postInfinity {animData.postInfinity};");

                        file.WriteLine(" keys {");
                        foreach (AnimKey key in animData.keys)
                        {
                            // TODO: fixed splines
                            string tanin = key.intan == "fixed" ? " " + key.t1 + " " + key.w1 : "";
                            string tanout = key.outtan == "fixed" ? " " + key.t2 + " " + key.w2 : "";
                            file.WriteLine($" {key.input} {key.output:N6} {key.intan} {key.outtan} 1 1 0{tanin}{tanout};");
                        }
                        file.WriteLine(" }");

                        file.WriteLine("}");
                    }
                    Row++;
                }
            }
        }

        public static IOAnimation ImportAnimation(string filePath, ImportSettings settings)
        {
            var anim = new MayaAnim();
            anim.Open(filePath);
            return anim.GetAnimation();
        }

        public IOAnimation GetAnimation()
        {
            IOAnimation anim = new IOAnimation();
            anim.Name = this.Name;
            foreach (var mayaAnimBone in this.Bones)
            {
                IOAnimation animBone = new IOAnimation();
                animBone.Name = mayaAnimBone.name;
                anim.Groups.Add(animBone);

                foreach (var att in mayaAnimBone.atts)
                    animBone.Tracks.Add(GetTrack(att));
            }
            return anim;
        }

        private IOAnimationTrack GetTrack(AnimData data)
        {
            IOAnimationTrack track = new IOAnimationTrack();
            switch (data.type)
            {
                case TrackType.translateX: track.ChannelType = IOAnimationTrackType.PositionX; break;
                case TrackType.translateY: track.ChannelType = IOAnimationTrackType.PositionY; break;
                case TrackType.translateZ: track.ChannelType = IOAnimationTrackType.PositionZ; break;
                case TrackType.rotateX: track.ChannelType = IOAnimationTrackType.RotationEulerX; break;
                case TrackType.rotateY: track.ChannelType = IOAnimationTrackType.RotationEulerY; break;
                case TrackType.rotateZ: track.ChannelType = IOAnimationTrackType.RotationEulerZ; break;
                case TrackType.scaleX: track.ChannelType = IOAnimationTrackType.ScaleX; break;
                case TrackType.scaleY: track.ChannelType = IOAnimationTrackType.ScaleY; break;
                case TrackType.scaleZ: track.ChannelType = IOAnimationTrackType.ScaleZ; break;
                case TrackType.visibility: track.ChannelType = IOAnimationTrackType.NodeVisibility; break;
            }
            track.PreWrap = CurveWrapModes.FirstOrDefault(x => x.Value == data.preInfinity).Key;
            track.PostWrap = CurveWrapModes.FirstOrDefault(x => x.Value == data.postInfinity).Key;

            foreach (var key in data.keys)
            {
                if (key.intan == "fixed" || key.outtan == "fixed")
                {
                    track.KeyFrames.Add(new IOKeyFrameHermite()
                    {
                        Frame = key.input - header.startTime,
                        Value = GetOutputValue(this, data, key.output),
                        TangentSlopeInput = key.t1,
                        TangentSlopeOutput = key.t2,
                        TangentWeightInput = key.w1,
                        TangentWeightOutput = key.w2,
                    });
                }
                else
                {
                    track.KeyFrames.Add(new IOKeyFrame()
                    {
                        Frame = key.input - header.startTime,
                        Value = GetOutputValue(this, data, key.output),
                    });
                }
            }

            return track;
        }

        static Dictionary<IOCurveWrapMode, InfinityType> CurveWrapModes = new Dictionary<IOCurveWrapMode, InfinityType>()
        {
            { IOCurveWrapMode.Constant, InfinityType.constant },
            { IOCurveWrapMode.Linear, InfinityType.linear },
            { IOCurveWrapMode.Cycle, InfinityType.cycle },
            { IOCurveWrapMode.CycleRelative, InfinityType.cycleRelative },
            { IOCurveWrapMode.Oscillate, InfinityType.oscillate },
        };

        private float GetOutputValue(MayaAnim anim, AnimData data, float value)
        {
            if (data.output == OutputType.angular)
            {
                if (anim.header.angularUnit == "deg")
                    return (float)(value * Math.PI / 180);
            }
            return value;
        }

        public static void ExportAnimation(string filePath, ExportSettings settings, IOAnimation animation, IOSkeleton skeleton)
        {
            var anim = CreateMayaAnimation(settings, animation, skeleton);
            anim.Save(filePath);
        }

        static MayaAnim CreateMayaAnimation(ExportSettings settings, IOAnimation animation, IOSkeleton skeleton)
        {
            MayaAnim anim = new MayaAnim();
            anim.header.startTime = 0;
            anim.header.endTime = animation.GetFrameCount();

            bool is2015 = false;

            // get bone order
            List<IOBone> BonesInOrder = getBoneTreeOrder(skeleton);
            if (is2015)
                BonesInOrder = BonesInOrder.OrderBy(f => f.Name, StringComparer.Ordinal).ToList();

            foreach (IOBone b in BonesInOrder)
            {
                AnimBone animBone = new AnimBone()
                {
                    name = b.Name
                };
                anim.Bones.Add(animBone);

                var group = animation.Groups.FirstOrDefault(x => x.Name.Equals(b.Name));
                if (group == null)
                    continue;

                foreach (var track in group.Tracks)
                {
                    switch (track.ChannelType)
                    {
                        case IOAnimationTrackType.PositionX:
                            AddAnimData(settings, animBone, track, ControlType.translate, TrackType.translateX);
                            break;
                        case IOAnimationTrackType.PositionY:
                            AddAnimData(settings, animBone, track, ControlType.translate, TrackType.translateY);
                            break;
                        case IOAnimationTrackType.PositionZ:
                            AddAnimData(settings, animBone, track, ControlType.translate, TrackType.translateZ);
                            break;
                        case IOAnimationTrackType.RotationEulerX:
                            AddAnimData(settings, animBone, track, ControlType.rotate, TrackType.rotateX);
                            break;
                        case IOAnimationTrackType.RotationEulerY:
                            AddAnimData(settings, animBone, track, ControlType.rotate, TrackType.rotateY);
                            break;
                        case IOAnimationTrackType.RotationEulerZ:
                            AddAnimData(settings, animBone, track, ControlType.rotate, TrackType.rotateZ);
                            break;
                        case IOAnimationTrackType.ScaleX:
                            AddAnimData(settings, animBone, track, ControlType.scale, TrackType.scaleX);
                            break;
                        case IOAnimationTrackType.ScaleY:
                            AddAnimData(settings, animBone, track, ControlType.scale, TrackType.scaleY);
                            break;
                        case IOAnimationTrackType.ScaleZ:
                            AddAnimData(settings, animBone, track, ControlType.scale, TrackType.scaleZ);
                            break;
                    }
                }
            }
            return anim;
        }

        static void AddAnimData(ExportSettings settings, AnimBone animBone, IOAnimationTrack track, ControlType ctype, TrackType ttype)
        {
            AnimData d = new AnimData();
            d.controlType = ctype;
            d.type = ttype;
            d.preInfinity = CurveWrapModes[track.PreWrap];
            d.postInfinity = CurveWrapModes[track.PostWrap];
            //Check if any tangents include weights.
            d.weighted = track.KeyFrames.Any(x => x is IOKeyFrameHermite && ((IOKeyFrameHermite)x).IsWeighted);

            bool isAngle = ctype == ControlType.rotate;
            if (isAngle)
                d.output = OutputType.angular;

            float value = track.KeyFrames.Count > 0 ? (float)track.KeyFrames[0].Value : 0;

            bool IsConstant = true;
            foreach (var key in track.KeyFrames)
            {
                if ((float)key.Value != value) {
                    IsConstant = false;
                    break;
                }
            }
            foreach (var key in track.KeyFrames)
            {
                AnimKey animKey = new AnimKey()
                {
                    input = key.Frame + 1,
                    output = isAngle ? GetAngle(settings, (float)key.Value) : (float)key.Value,
                };
                if (key is IOKeyFrameHermite)
                {
                    animKey.intan = "fixed";
                    animKey.outtan = "fixed";
                    animKey.t1 = ((IOKeyFrameHermite)key).TangentSlopeInput;
                    animKey.t2 = ((IOKeyFrameHermite)key).TangentSlopeOutput;
                    animKey.w1 = ((IOKeyFrameHermite)key).TangentWeightInput;
                    animKey.w2 = ((IOKeyFrameHermite)key).TangentWeightOutput;
                }
                d.keys.Add(animKey);
                if (IsConstant)
                    break;
            }

            if (d.keys.Count > 0)
                animBone.atts.Add(d);
        }

        private static float GetAngle(ExportSettings settings, float value) {
            return (settings.MayaAnimUseRadians ? value : (float)(value * (180 / Math.PI)));
        }

        private static List<IOBone> getBoneTreeOrder(IOSkeleton Skeleton)
        {
            if (Skeleton.RootBones.Count == 0)
                return null;
            List<IOBone> bone = new List<IOBone>();
            Queue<IOBone> q = new Queue<IOBone>();

            foreach (IOBone b in Skeleton.RootBones)
            {
                QueueBones(b, q, Skeleton);
            }

            while (q.Count > 0)
            {
                bone.Add(q.Dequeue());
            }
            return bone;
        }

        public static void QueueBones(IOBone b, Queue<IOBone> q, IOSkeleton Skeleton)
        {
            q.Enqueue(b);
            foreach (IOBone c in b.Children)
                QueueBones(c, q, Skeleton);
        }
    }
}
