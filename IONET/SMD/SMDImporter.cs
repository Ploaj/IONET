using IONET.Core;
using IONET.Core.Model;
using IONET.Core.Skeleton;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace IONET.SMD
{
    /// <summary>
    /// 
    /// </summary>
    public class SMDImporter : ISceneLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "StudioMdl";
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
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Verify(string filePath)
        {
            return Path.GetExtension(filePath).ToLower().Equals(".smd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IOScene GetScene(string filePath)
        {
            IOScene scene = new IOScene();

            IOModel model = new IOModel();
            scene.Models.Add(model);

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            using (StreamReader r = new StreamReader(stream))
            {
                Dictionary<string, IOMesh> nameToMesh = new Dictionary<string, IOMesh>();
                HashSet<IOBone> meshBones = new HashSet<IOBone>();
                Dictionary<int, IOBone> idxToBone = new Dictionary<int, IOBone>();
                Dictionary<int, int> idxToParent = new Dictionary<int, int>();

                string mode = "";
                int time = 0;

                while (!r.EndOfStream)
                {
                    // read and clean line args
                    var line = r.ReadLine().Trim();
                    var args = Regex.Replace(line, @"\s+", " ").Split(' ');

                    // check for grouping
                    switch (args[0])
                    {
                        case "nodes":
                        case "skeleton":
                        case "triangles":
                        case "end":
                            mode = args[0];
                            break;
                    }

                    switch (mode)
                    {
                        case "nodes":
                            if(args.Length >= 3)
                            {
                                args = line.Split('"');

                                var index = int.Parse(args[0].Trim());
                                var name = args[1];
                                var parentIndex = int.Parse(args[2].Trim());

                                IOBone bone = new IOBone()
                                {
                                    Name = name
                                };

                                idxToBone.Add(index, bone);
                                idxToParent.Add(index, parentIndex);
                            }
                            break;
                        case "skeleton":
                            if (args.Length == 2 && args[0] == "time")
                            {
                                int.TryParse(args[1], out time);
                            }
                            if (args.Length == 7)
                            {
                                var index = int.Parse(args[0]);

                                if(time == 0)
                                {
                                    idxToBone[index].Translation = new System.Numerics.Vector3(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
                                    idxToBone[index].RotationEuler = new System.Numerics.Vector3(float.Parse(args[4]), float.Parse(args[5]), float.Parse(args[6]));
                                }
                            }
                            break;
                        case "triangles":
                            {
                                if(args.Length > 0 && args.Length < 9 && args[0] != "triangles")
                                {
                                    var material = string.Join(" ", args);

                                    var v1 = ParseVertex(r.ReadLine(), idxToBone, out IOBone parent);
                                    var v2 = ParseVertex(r.ReadLine(), idxToBone, out parent);
                                    var v3 = ParseVertex(r.ReadLine(), idxToBone, out parent);

                                    var meshName = parent.Name + material;

                                    if(!meshBones.Contains(parent))
                                        meshBones.Add(parent);

                                    meshName = Regex.Replace(meshName.Trim(), @"\s+", "_").Replace("#", "");

                                    if (!nameToMesh.ContainsKey(meshName))
                                    {
                                        // create and load material
                                        IOMaterial mat = new IOMaterial()
                                        {
                                            Name = material
                                        };
                                        scene.Materials.Add(mat);

                                        // create io mesh
                                        var iomesh = new IOMesh()
                                        {
                                            Name = meshName
                                        };

                                        // create triangle polygon
                                        iomesh.Polygons.Add(new IOPolygon()
                                        {
                                            MaterialName = material,
                                            PrimitiveType = IOPrimitive.TRIANGLE
                                        });

                                        nameToMesh.Add(meshName, iomesh);
                                    }

                                    var mesh = nameToMesh[meshName];
                                    mesh.Polygons[0].Indicies.Add(mesh.Vertices.Count);
                                    mesh.Polygons[0].Indicies.Add(mesh.Vertices.Count + 1);
                                    mesh.Polygons[0].Indicies.Add(mesh.Vertices.Count + 2);
                                    mesh.Vertices.Add(v1);
                                    mesh.Vertices.Add(v2);
                                    mesh.Vertices.Add(v3);
                                }
                            }
                            break;
                    }
                }

                // create skeleton hierarchy
                foreach(var bone in idxToBone)
                {
                    var parent = idxToParent[bone.Key];

                    if(parent == -1)
                    {
                        if (meshBones.Count > 1 && meshBones.Contains(bone.Value))
                            continue;
                        else
                            model.Skeleton.RootBones.Add(bone.Value);
                    }
                    else
                    {
                        idxToBone[parent].AddChild(bone.Value);
                    }
                }

                // dump mesh
                model.Meshes.AddRange(nameToMesh.Values);
            }

            return scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IOVertex ParseVertex(string line, Dictionary<int, IOBone> idxToBones, out IOBone parentBone)
        {
            var args = Regex.Replace(line.Trim(), @"\s+", " ").Split(' ');
            
            IOVertex vertex = new IOVertex();
            
            // parse parent index
            int parent = int.Parse(args[0]);

            // parse attributes
            vertex.Position = new System.Numerics.Vector3(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]));
            vertex.Normal = new System.Numerics.Vector3(float.Parse(args[4]), float.Parse(args[5]), float.Parse(args[6]));
            vertex.UVs.Add(new System.Numerics.Vector2(float.Parse(args[7]), float.Parse(args[8])));

            // transform by parent so we can ignore it
            if(parent != -1)
            {
                vertex.Transform(idxToBones[parent].WorldTransform);
                parentBone = idxToBones[parent];
            }
            else
            {
                parentBone = null;
            }

            // parse weights
            if(args.Length >= 10)
            {
                int links = int.Parse(args[9]);

                for (int i = 0; i < links; i++)
                {
                    vertex.Envelope.Weights.Add(new IOBoneWeight()
                    {
                        BoneName = idxToBones[int.Parse(args[10 + i * 2])].Name,
                        Weight = float.Parse(args[11 + i * 2])
                    });
                }
            }

            return vertex;
        }
    }
}
