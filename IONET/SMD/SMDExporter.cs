using IONET.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace IONET.SMD
{
    public class SMDExporter : IModelExporter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="filePath"></param>
        public void ExportScene(IOScene scene, string filePath)
        {
            if (scene.Models.Count == 0)
                return;

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            using (StreamWriter w = new StreamWriter(stream))
            {
                var model = scene.Models[0];

                w.WriteLine("version 1");


                var bones = model.Skeleton.BreathFirstOrder();

                Dictionary<string, int> nodeToID = new Dictionary<string, int>();

                w.WriteLine("nodes");
                var index = 0;
                foreach(var b in bones)
                {
                    nodeToID.Add(b.Name, index);
                    w.WriteLine($"{index++} \"{b.Name}\" {model.Skeleton.IndexOf(b.Parent)}");
                }
                foreach (var b in model.Meshes)
                {
                    nodeToID.Add(b.Name, index);
                    w.WriteLine($"{index++} \"{b.Name}\" {-1}");
                }
                w.WriteLine("end");


                w.WriteLine("skeleton");

                w.WriteLine("time 0");
                index = 0;
                foreach (var b in bones)
                {
                    // since we can't store scale we have to improvise and extract the world scale
                    Matrix4x4.Decompose(b.WorldTransform, out Vector3 uniformScale, out Quaternion rot, out Vector3 pos);
                    w.WriteLine($"{index++} {b.TranslationX * uniformScale.X} {b.TranslationY * uniformScale.Y} {b.TranslationZ * uniformScale.Z} {b.RotationEuler.X} {b.RotationEuler.Y} {b.RotationEuler.Z}");
                }

                foreach (var b in model.Meshes)
                    w.WriteLine($"{index++} 0 0 0 0 0 0");

                w.WriteLine("end");


                if(model.Meshes.Count > 0)
                {
                    w.WriteLine("triangles");

                    foreach(var m in model.Meshes)
                    {
                        // smd only supports triangles
                        m.MakeTriangles();

                        // look though all poly groups
                        for (int p = 0; p < m.Polygons.Count; p ++)
                        {
                            var poly = m.Polygons[p];
                            for (int i = 0; i < poly.Indicies.Count; i += 3)
                            {
                                if(string.IsNullOrEmpty(poly.MaterialName))
                                    w.WriteLine($"{m.Name}_poly_{p}");
                                else
                                    w.WriteLine($"{poly.MaterialName}");

                                {
                                    var v1 = m.Vertices[poly.Indicies[i]];
                                    var env = v1.Envelope;
                                    w.WriteLine($"{nodeToID[m.Name]} {v1.Position.X} {v1.Position.Y} {v1.Position.Z} {v1.Normal.X} {v1.Normal.Y} {v1.Normal.Z} {(v1.UVs.Count > 0 ? v1.UVs[0].X : 0)} {(v1.UVs.Count > 0 ? v1.UVs[0].Y : 0)} {env.Weights.Count + " " + string.Join(" ", env.Weights.Select(e => nodeToID[e.BoneName] + " " + e.Weight))}");
                                }
                                {
                                    var v1 = m.Vertices[poly.Indicies[i + 1]];
                                    var env = v1.Envelope;
                                    w.WriteLine($"{nodeToID[m.Name]} {v1.Position.X} {v1.Position.Y} {v1.Position.Z} {v1.Normal.X} {v1.Normal.Y} {v1.Normal.Z} {(v1.UVs.Count > 0 ? v1.UVs[0].X : 0)} {(v1.UVs.Count > 0 ? v1.UVs[0].Y : 0)} {env.Weights.Count + " " + string.Join(" ", env.Weights.Select(e => nodeToID[e.BoneName] + " " + e.Weight))}");
                                }
                                {
                                    var v1 = m.Vertices[poly.Indicies[i + 2]];
                                    var env = v1.Envelope;
                                    w.WriteLine($"{nodeToID[m.Name]} {v1.Position.X} {v1.Position.Y} {v1.Position.Z} {v1.Normal.X} {v1.Normal.Y} {v1.Normal.Z} {(v1.UVs.Count > 0 ? v1.UVs[0].X : 0)} {(v1.UVs.Count > 0 ? v1.UVs[0].Y : 0)} {env.Weights.Count + " " + string.Join(" ", env.Weights.Select(e => nodeToID[e.BoneName] + " " + e.Weight))}");
                                }
                            }
                        }
                    }

                    w.WriteLine("end");
                }
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetExtensions()
        {
            return new string[] { ".smd" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "StudioMdl";
        }
    }
}
