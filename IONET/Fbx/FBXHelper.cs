using IONET.Core.IOMath;
using IONET.Core.Model;
using IONET.Core.Skeleton;
using IONET.Fbx.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using IONET.Core.Animation;

namespace IONET.Fbx
{
    /// <summary>
    /// Helper class for scrapping relevent information from fbx files
    /// Current known to work with Version 7400 and 6100 but other may work as well
    /// </summary>
    public class FbxHelper
    {
        private FbxDocument _document;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, List<string>> Connections = new Dictionary<string, List<string>>();

        /// <summary>
        /// Number of properties used to describe a node
        /// Different depending on fbx version
        /// </summary>
        private int PropertyDescSize
        {
            get
            {
                if(Version > 7000)
                    return 4;

                return 3;
            }
        }

        /// <summary>
        /// Number of properties used to describe a node
        /// Different depending on fbx version
        /// </summary>
        private int NodeDescSize
        {
            get
            {
                if (Version > 7000)
                    return 3;

                return 2;
            }
        }

        /// <summary>
        /// Numer used to describe the abount of units used in a frame
        /// Different depending on fbx version
        /// </summary>
        private long UnitsPerFrame
        {
            get
            {
                if (Version >= 7400)
                    return 1539538600;

                return 1924423250;
            }
        }

        /// <summary>
        /// Gets FBX document version
        /// </summary>
        public int Version
        {
            get; internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public FbxHelper(FbxDocument document)
        {
            // set document
            _document = document;

            // get and store sfbx version
            var ver = _document.GetNodesByName("FBXVersion");
            if (ver.Length > 0)
                Version = (int)ver[0].Value;
            else
                Version = -1;

            // get connectors
            var connections = _document.GetNodesByName("C");
            if (connections.Length == 0)
                connections = _document.GetNodesByName("Connect");

            // use strings for backwards compatibility
            foreach (var oo in connections)
            {
                if (!Connections.ContainsKey(oo.Properties[1].ToString()))
                    Connections.Add(oo.Properties[1].ToString(), new List<string>());

                Connections[oo.Properties[1].ToString()].Add(oo.Properties[2].ToString());
            }
        }

        /// <summary>
        /// Gets a list of animations from the fbx animation stack.
        /// </summary>
        /// <returns></returns>
        public List<IOAnimation> GetAnimations()
        {
            List<IOAnimation> anims = new List<IOAnimation>();

            foreach (var node in _document.GetNodesByName("AnimationStack")) {
                //Create the animation.
                IOAnimation currentAnim = LoadAnimationStack(node);
                anims.Add(currentAnim);

                //Use the stop amount from the animation properties for the end frame.
                currentAnim.EndFrame = ConvertTimeUnits((long)node["Properties70"].GetNodesByValue("LocalStop")[0].Properties[4]);

                //AnimationLayer
                foreach (var layer in GetChildConnections(node))
                {
                    IOAnimation currentGroup = LoadAnimationLayer(node);
                    currentAnim.Groups.Add(currentGroup);

                    //AnimationCurveNode
                    foreach (var curveNode in GetChildConnections(layer))
                    {
                        //All tracks have the same animated object reference. 
                        //Set it per group still.
                        currentGroup.Name = FindAnimatedObject(curveNode);

                        //Only care about the node type.
                        string trackNodeType = curveNode.Properties[NodeDescSize - 2].ToString();
                        //Go through each AnimationCurve
                        var curves = GetChildConnections(curveNode);
                        for (int i = 0; i < curves.Count; i++)
                            currentGroup.Tracks.Add(LoadAnimationCurve(curves[i], trackNodeType, i));
                    }
                }
            }

            return anims;
        }

        private string FindAnimatedObject(FbxNode curveNode)
        {
            //Find the object connected to the animation node
            foreach (var n in GetParentConnections(curveNode))
            {
                switch (n.Name)
                {
                    case "AnimationLayer":
                        break;
                    //Model type which can be a Mesh or Limb. Extract the name itself
                    case "Model":
                    case "NodeAttribute":
                        return GetNameWithoutNamespace(n.Properties[NodeDescSize - 2].ToString());
                    default:
                        Console.WriteLine($"Unsupported link type {n.Name}");
                        break;
                }
            }
            return "";
        }

        private IOAnimation LoadAnimationStack(FbxNode node)
        {
            return new IOAnimation()
            {
                Name = GetNameWithoutNamespace(node.Properties[NodeDescSize - 2].ToString())

            };
        }

        private IOAnimation LoadAnimationLayer(FbxNode node)
        {
            return new IOAnimation()
            {
                Name = GetNameWithoutNamespace(node.Properties[NodeDescSize - 2].ToString())
            };
        }

        private IOAnimationTrack LoadAnimationCurve(FbxNode node, string trackNodeType, int trackIndex)
        {
            IOAnimationTrack track = new IOAnimationTrack();
            //Track types via the parent curve node.
            //Each child curve track has an index which assigns to X/Y/Z
            switch (trackNodeType)
            {
                case "AnimCurveNode::T":
                    if (trackIndex == 0) track.ChannelType = IOAnimationTrackType.PositionX;
                    if (trackIndex == 1) track.ChannelType = IOAnimationTrackType.PositionY;
                    if (trackIndex == 2) track.ChannelType = IOAnimationTrackType.PositionZ;
                    break;
                case "AnimCurveNode::R":
                    if (trackIndex == 0) track.ChannelType = IOAnimationTrackType.RotationEulerX;
                    if (trackIndex == 1) track.ChannelType = IOAnimationTrackType.RotationEulerY;
                    if (trackIndex == 2) track.ChannelType = IOAnimationTrackType.RotationEulerZ;
                    break;
                case "AnimCurveNode::S":
                    if (trackIndex == 0) track.ChannelType = IOAnimationTrackType.ScaleX;
                    if (trackIndex == 1) track.ChannelType = IOAnimationTrackType.ScaleY;
                    if (trackIndex == 2) track.ChannelType = IOAnimationTrackType.ScaleZ;
                    break;
            }

            //Duriation of the key frame
            var keyTime = node["KeyTime"].Value as long[];
            //The key data
            var keyFloats = node["KeyValueFloat"].Value as float[];
            //Determines how a key is handled
            var keyAttributes = node["KeyAttrDataFloat"].Value as float[];
            //How many references used in each attribute
            var keyAttributeRefs = node["KeyAttrRefCount"].Value as int[];
            //The flags for key data. These determine interpolation info
            var keyAttributeFlags = node["KeyAttrFlags"].Value as int[];

            int k = 0;
            for (int i = 0; i < keyAttributeRefs.Length; i++)
            {
                //4 attributes. 
                float rightSlope = keyAttributes[4 * i + 0];
                float nextLeftSlope = keyAttributes[4 * i + 1];
                //Tangent data is encoded as an int. Convert float to int.
                int tangentWeightData = BitConverter.ToInt32(BitConverter.GetBytes(keyAttributes[4 * i + 2]), 0);
                //Decode the weights.
                float weight1 = (tangentWeightData & 0x0000ffff) / 9999.0f;
                float weight2 = ((tangentWeightData >> 16) & 0xffff) / 9999.0f;

                int flags = keyAttributeFlags[i];
                var attrCount = keyAttributeRefs[i];

                var interpolation = (EInterpolationType)(flags & 0x0000000e);
                for (int j = 0; j < attrCount; j++)
                {
                    //Time to frame units
                    float frame = ConvertTimeUnits(keyTime[k]);
                    float time = frame == 0 ? 0 : frame / 24.0f;
                    //The raw key value
                    float value = keyFloats[k];
                    //Create key frames.
                    switch (interpolation)
                    {
                        case EInterpolationType.eInterpolationCubic: //Todo figure out tangents from attribute data
                            track.KeyFrames.Add(new IOKeyFrameHermite()
                            {
                                Frame = frame,
                                Time = time,
                                Value = value,
                                TangentSlopeInput = rightSlope,
                                TangentSlopeOutput = nextLeftSlope,
                                TangentWeightInput = weight1,
                                TangentWeightOutput = weight2,
                            });
                            break;
                        default:
                            track.KeyFrames.Add(new IOKeyFrame()
                            {
                                Frame = frame,
                                Time = time,
                                Value = value,
                            });
                            break;
                    }
                    k++;
                }
            }
            return track;
        }

        /// <summary>
        /// Extracts skeleton information from fbx file
        /// </summary>
        /// <returns></returns>
        public IOSkeleton GetSkeleton()
        {
            // search all limb nodes
            var limbs = _document.GetNodesByName("Model").Where(e => e.Properties.Count >= NodeDescSize && e.Properties[NodeDescSize - 1].ToString().Contains("Limb"));

            // map the id to the newly create bone
            Dictionary<string, IOBone> idToBone = new Dictionary<string, IOBone>();

            // convert nodes to bones
            foreach(var l in limbs)
                idToBone.Add(l.Value.ToString(), CreateBoneFromNode(l));

            // create io skeleton
            var skeleton = new IOSkeleton();

            // make connections
            foreach (var v in idToBone)
            {
                if (Connections.ContainsKey(v.Key) && Connections[v.Key].Count > 0 && idToBone.ContainsKey(Connections[v.Key][0]))
                    idToBone[Connections[v.Key][0]].AddChild(v.Value);
                else
                    skeleton.RootBones.Add(v.Value);
            }

            // account for blender's armature node
            var nullParent = GetBoneParentTransform();
            foreach (var skel in skeleton.RootBones)
                skel.LocalTransform *= nullParent;
            
            return skeleton;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private IOBone CreateBoneFromNode(FbxNode l)
        {
            IOBone bone = new IOBone()
            {
                Name = GetNameWithoutNamespace(l.Properties[NodeDescSize - 2].ToString())
            };

            // scan and extract info from properties
            var properties = l.GetNodesByName("P");
            if (properties.Length == 0)
                properties = l.GetNodesByName("Property");

            Quaternion preRot = new Quaternion(0, 0, 0, 1);
            Quaternion postRot = new Quaternion(0, 0, 0, 1);

            foreach (var prop in properties)
            {
                if (prop.Properties.Count < PropertyDescSize)
                    continue;
                
                if (prop.Properties[0].ToString() == "PreRotation")
                    preRot = MathExt.FromEulerAngles(
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 1]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 2]));

                if (prop.Properties[0].ToString() == "PostRotation")
                    postRot = MathExt.FromEulerAngles(
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 1]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 2]));
                
                if (prop.Properties[0].ToString() == "Lcl Translation")
                    bone.Translation = new Vector3((float)(double)prop.Properties[PropertyDescSize], (float)(double)prop.Properties[PropertyDescSize + 1], (float)(double)prop.Properties[PropertyDescSize + 2]);

                if (prop.Properties[0].ToString() == "Lcl Rotation")
                    bone.RotationEuler = new Vector3(
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 1]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 2]));

                if (prop.Properties[0].ToString() == "Lcl Scaling")
                    bone.Scale = new Vector3((float)(double)prop.Properties[PropertyDescSize], (float)(double)prop.Properties[PropertyDescSize + 1], (float)(double)prop.Properties[PropertyDescSize + 2]);

            }

            bone.Rotation = preRot * bone.Rotation * postRot;

            return bone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 GetBoneParentTransform()
        {
            // get limbs
            var limbs = _document.GetNodesByName("Model").Where(e => e.Properties.Count >= NodeDescSize && e.Properties[NodeDescSize - 1].ToString().Contains("Limb")).Select(e=>e.Value.ToString());

            // get null model nodes
            var nulls = _document.GetNodesByName("Model").Where(e => e.Properties.Count >= NodeDescSize && e.Properties[NodeDescSize - 1].ToString().Contains("Null"));
            
            // convert nodes to bones
            foreach (var n in nulls)
            {
                foreach (var l in limbs)
                {
                    if (Connections.ContainsKey(l) && Connections[l].Count > 0)
                    {
                        var con = Connections[l];

                        if (con.Contains(n.Value.ToString()))
                        {
                            System.Diagnostics.Debug.WriteLine("Found Bone");
                            return CreateBoneFromNode(n).LocalTransform;
                        }
                    }
                }
            }

            // nothing found
            return Matrix4x4.Identity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IOMesh> ExtractMesh()
        {
            // gather mesh objects
            var mesh = _document.GetNodesByName("Model").Where(e => e.Properties.Count >= NodeDescSize && e.Properties[NodeDescSize - 1].ToString() == "Mesh");
            
            List<IOMesh> meshes = new List<IOMesh>();

            var subdeformers = _document.GetNodesByName("SubDeformer");

            // extract geometry information into iomesh
            foreach (var m in mesh)
            {
                // generate mesh
                IOMesh iomesh = new IOMesh()
                {
                    Name = GetNameWithoutNamespace(m.Properties[NodeDescSize - 2].ToString())
                };
                meshes.Add(iomesh);

                // reverse search material
                var material = "";
                var materials = GetChildConnections(m).Find(e => e.Name == "Material");
                if (materials != null)
                    material = GetNameWithoutNamespace(materials.Properties[NodeDescSize - 2].ToString());


                // collect geometry information
                // in older fbx this was stored in the mesh node
                // in newer versions it's in its own node

                var geoms = _document.GetNodesByName("Geometry").Where(e => IsParentedTo(e.Properties[0].ToString(), m.Properties[0].ToString())).ToArray();

                if(geoms.Length > 0)
                {
                    foreach (var g in geoms)
                    {
                        ProcessGeometry(g, out IOPolygon poly, out List<IOVertex> verts);
                        for (int i = 0; i < poly.Indicies.Count; i++)
                            poly.Indicies[i] += iomesh.Vertices.Count;
                        iomesh.Vertices.AddRange(verts);
                        poly.MaterialName = material;
                        iomesh.Polygons.Add(poly);
                    }
                }
                else
                {
                    ProcessGeometry(m, out IOPolygon poly, out List<IOVertex> verts);
                    poly.MaterialName = material;
                    iomesh.Vertices = verts;
                    iomesh.Polygons.Add(poly);
                }

                // get transform for this node
                var tra = CreateBoneFromNode(m);
                iomesh.TransformVertices(tra.WorldTransform);
            }


            return meshes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<FbxNode> GetParentConnections(FbxNode m)
        {
            List<FbxNode> nodes = new List<FbxNode>();
            if (Connections.ContainsKey(m.Value.ToString()))
            {
                foreach (var con in Connections[m.Value.ToString()])
                {
                    var node = _document.GetNodesByValue(con);
                    if (node != null)
                        nodes.AddRange(node);
                }
            }
            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<FbxNode> GetChildConnections(FbxNode m)
        {
            List<FbxNode> nodes = new List<FbxNode>();

            foreach (var con in Connections)
            {
                if (con.Value.Contains(m.Value.ToString()))
                {
                    var node = _document.GetNodesByValue(con.Key);

                    if(node != null)
                        nodes.AddRange(node);
                }
            }
            
            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessGeometry(FbxNode node, out IOPolygon poly, out List<IOVertex> verts)
        {
            double[] vertices;
            int[] indices;

            // load vertices
            if (node["Vertices"].Value is double[])
                vertices = (double[])node["Vertices"].Value;
            else
                vertices = node["Vertices"].Properties.Select(e=>(double)e).ToArray();


            // load vertex indices
            if (node["PolygonVertexIndex"].Value is int[])
                indices = (int[])node["PolygonVertexIndex"].Value;
            else
                indices = node["PolygonVertexIndex"].Properties.Select(e => (int)e).ToArray();

            
            // get binds
            var deformers = GetChildConnections(node).Where(e=>e.Name == "Deformer");

            List<Dictionary<int, Tuple<float, string>>> deforms = new List<Dictionary<int, Tuple<float, string>>>();

            foreach (var par in deformers)
            {
                foreach(var sub in GetChildConnections(par))
                {
                    // get bone name
                    var name = "";
                    foreach (var mod in GetChildConnections(sub))
                        if(mod.Name == "Model")
                            name = GetNameWithoutNamespace(mod.Properties[NodeDescSize - 2].ToString());

                    if (!string.IsNullOrEmpty(name) && sub["Indexes"] != null)
                    {
                        // create deform map
                        Dictionary<int, Tuple<float, string>> deformmap = new Dictionary<int, Tuple<float, string>>();

                        // indices
                        int[] ind;
                        if (sub["Indexes"].Value is int[])
                            ind = (int[])sub["Indexes"].Value;
                        else
                            ind = sub["Indexes"].Properties.Select(e => (int)e).ToArray();

                        // weights
                        float[] weights;
                        if (sub["Weights"].Value is double[])
                            weights = ((double[])sub["Weights"].Value).Select(e => (float)e).ToArray();
                        else
                            weights = sub["Weights"].Properties.Select(e => (float)(double)e).ToArray();
                        
                        // generate map
                        for(int i = 0; i < weights.Length; i++)
                        {
                            deformmap.Add(ind[i], new Tuple<float, string>(weights[i], name));
                        }

                        // add deform entry
                        deforms.Add(deformmap);
                    }
                }
            }

            // generate polygon
            poly = new IOPolygon();
            verts = new List<IOVertex>();

            poly.PrimitiveType = IOPrimitive.TRIANGLE;
            
            // process primitives and convert to triangles
            var primLength = 0;
            for (int i = 0; i < indices.Length; i++)
            {
                var idx1 = indices[i];
                
                primLength++;

                if (idx1 < 0)
                {
                    switch(primLength)
                    {
                        case 4:
                            // convert quad to triangle
                            poly.Indicies.Add(i - 3);
                            poly.Indicies.Add(i - 2);
                            poly.Indicies.Add(i - 1);
                            poly.Indicies.Add(i - 3);
                            poly.Indicies.Add(i - 1);
                            poly.Indicies.Add(i);
                            break;
                        case 3:
                            // triangle
                            poly.Indicies.Add(i - 2);
                            poly.Indicies.Add(i - 1);
                            poly.Indicies.Add(i);
                            break;
                        default:
                            // tri strip
                            for (var vi = i - primLength; vi < i - 2; vi++)
                                if ((vi & 1) != 0)
                                {
                                    poly.Indicies.Add(vi);
                                    poly.Indicies.Add(vi + 1);
                                    poly.Indicies.Add(vi + 2);
                                }
                                else
                                {
                                    poly.Indicies.Add(vi);
                                    poly.Indicies.Add(vi + 2);
                                    poly.Indicies.Add(vi + 1);
                                }
                            break;
                    }

                    idx1 = Math.Abs(idx1) - 1; // xor -1

                    primLength = 0;
                }

                IOVertex v = new IOVertex()
                {
                    Position = new Vector3((float)vertices[idx1 * 3], (float)vertices[idx1 * 3 + 1], (float)vertices[idx1 * 3 + 2])
                };

                // find deforms
                foreach (var map in deforms)
                {
                    if (map.ContainsKey(idx1))
                    {
                        v.Envelope.Weights.Add(new Core.IOBoneWeight()
                        {
                            Weight = map[idx1].Item1,
                            BoneName = map[idx1].Item2,
                        });
                    }
                }
                

                verts.Add(v);
            }


            // get layer information
            ProcessLayer(node, "Normal", indices, verts);
            ProcessLayer(node, "Tangent", indices, verts);
            ProcessLayer(node, "Binormal", indices, verts);
            ProcessLayer(node, "UV", indices, verts);
            ProcessLayer(node, "Color", indices, verts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private string GetLayerNodeName(string layer)
        {
            switch (layer)
            {
                case "Normal":
                case "Tangent":
                case "Binormal":
                case "Color":
                    return layer + "s";
            }
            return layer;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessLayer(FbxNode node, string layer, int[] indices, List<IOVertex> verts)
        {
            var normalLayers = node.GetNodesByName($"LayerElement{layer}");

            foreach(var lay in normalLayers)
            {
                // layer index
                var index = (int)lay.Properties[0];


                // get data
                var dataName = GetLayerNodeName(layer);
                double[] data;
                if (lay[$"{dataName}"].Value is double[])
                    data = (double[])lay[$"{dataName}"].Value;
                else
                    data = lay[$"{dataName}"].Properties.Select(e => (double)e).ToArray();
                

                // indexed
                Dictionary<int, int> indexToDirect = new Dictionary<int, int>();
                if (lay["ReferenceInformationType"].Properties[0].ToString() == "IndexToDirect")
                {
                    int[] ind;
                    if (lay[$"{layer}Index"].Value is int[])
                        ind = (int[])lay[$"{layer}Index"].Value;
                    else
                        ind = lay[$"{layer}Index"].Properties.Select(e => (int)e).ToArray();

                    for (int i = 0; i < ind.Length; i++)
                        indexToDirect.Add(i, ind[i]);
                }


                // process map
                for (int i = 0; i < indices.Length; i++)
                {
                    var idx = i;

                    switch (lay["MappingInformationType"].Properties[0].ToString())
                    {
                        case "ByVertice":
                            idx = indices[i];
                            if (idx < 0) idx = Math.Abs(idx) - 1;
                            break;
                        case "ByPolygonVertex":
                            break;
                    }

                    if (indexToDirect.ContainsKey(idx))
                        idx = indexToDirect[idx];

                    switch (layer)
                    {
                        case "Normal":
                            verts[i].Normal = new Vector3((float)data[idx * 3], (float)data[idx * 3 + 1], (float)data[idx * 3 + 2]);
                            break;
                        case "Tangent":
                            verts[i].Tangent = new Vector3((float)data[idx * 3], (float)data[idx * 3 + 1], (float)data[idx * 3 + 2]);
                            break;
                        case "Binormal":
                            verts[i].Binormal = new Vector3((float)data[idx * 3], (float)data[idx * 3 + 1], (float)data[idx * 3 + 2]);
                            break;
                        case "UV":
                            verts[i].SetUV((float)data[idx * 2], (float)data[idx * 2 + 1], index);
                            break;
                        case "Color":
                            verts[i].SetColor((float)data[idx * 4], (float)data[idx * 4 + 1], (float)data[idx * 4 + 2], (float)data[idx * 4 + 3], index);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IOMaterial> GetMaterials()
        {
            List<IOMaterial> materials = new List<IOMaterial>();

            foreach(var m in _document.GetNodesByName("Material"))
            {
                // generate material
                IOMaterial mat = new IOMaterial()
                {
                    Name = GetNameWithoutNamespace(m.Properties[NodeDescSize - 2].ToString())
                };

                // load material attributes
                if(m["ShadingModel"] != null && m["ShadingModel"].Properties[0].ToString().ToLower() == "phong")//ToLower Because it's sometimes "Phong" 
                {
                    var properties = m.GetNodesByName("P");
                    if (properties.Length == 0)
                        properties = m.GetNodesByName("Property");

                    foreach (var prop in properties)
                    {
                        switch (prop.Properties[0].ToString())
                        {
                            case "AmbientColor":
                                mat.AmbientColor = GetColor(prop);
                                break;
                            case "DiffuseColor":
                                mat.DiffuseColor = GetColor(prop);
                                break;
                            case "SpecularColor":
                                mat.SpecularColor = GetColor(prop);
                                break;
                            case "ReflectionColor":
                                mat.ReflectiveColor = GetColor(prop);
                                break;
                            case "Emissive":
                                mat.EmissionColor = GetColor(prop);
                                break;
                            case "Shininess":
                                mat.Shininess = (float)(double)prop.Properties[PropertyDescSize];
                                break;
                            case "Opacity":
                                mat.Alpha = (float)(double)prop.Properties[PropertyDescSize];
                                break;
                        }
                    }
                }

                // get textures

                var children = GetChildConnections(m).Where(e=>e.Name == "Texture");

                foreach(var t in children)
                {
                    // the property is stored in the connections
                    // for now always map it to diffuse

                    mat.DiffuseMap = new IOTexture()
                    {
                        Name = t.Properties[NodeDescSize - 2].ToString(),
                        FilePath = t["FileName"].Properties[0].ToString()
                    };
                }

                materials.Add(mat);
            }

            return materials;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private Vector4 GetColor(FbxNode prop)
        {
            return new Vector4((float)(double)prop.Properties[PropertyDescSize], (float)(double)prop.Properties[PropertyDescSize + 1], (float)(double)prop.Properties[PropertyDescSize + 2], 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetNameWithoutNamespace(string name)
        {
            name = Regex.Match(name.ToString(), @"(?<=(::))(.*)").Value;

            var match = Regex.Match(name, @"(.*)(?=\.[0-9][0-9][0-9])");
            if (match.Success)
                name = match.Value;

            return name;
        }

        /// <summary>
        /// Returns true if the child id is parented to the parent id in some way
        /// </summary>
        /// <param name="ChildID"></param>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        private bool IsParentedTo(string ChildID, string ParentID)
        {
            if (Connections.ContainsKey(ChildID))
            {
                foreach (var connection in Connections[ChildID])
                {
                    if (connection == ParentID || IsParentedTo(connection, ParentID))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Converts time units from a given long into a frame value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
         public float ConvertTimeUnits(long value) => (value / UnitsPerFrame);

        enum EInterpolationType
        {
            eInterpolationConstant = 0x00000002,
            eInterpolationLinear = 0x00000004,
            eInterpolationCubic = 0x00000008
        }
    }
}
