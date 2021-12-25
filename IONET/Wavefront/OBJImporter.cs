using IONET.Core;
using IONET.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace IONET.Wavefront
{
    public class OBJImporter : ISceneLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetExtensions()
        {
            return new string[] { ".obj" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IOScene GetScene(string filePath)
        {
            // create scene and model
            IOScene scene = new IOScene();

            // load materials
            var mtlFile = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".mtl");

            if (File.Exists(mtlFile))
                LoadMaterialLibrary(scene, mtlFile);

            // parse obj file
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            using (StreamReader r = new StreamReader(stream))
            {
                List<Vector3> v = new List<Vector3>();
                List<Vector2> vt = new List<Vector2>();
                List<Vector3> vn = new List<Vector3>();

                List<Tuple<string, string, Dictionary<IOPrimitive, List<int[]>>>> objects = new List<Tuple<string, string, Dictionary<IOPrimitive, List<int[]>>>>();

                var objName = "Mesh";
                var matNam = "";
                Dictionary<IOPrimitive, List<int[]>> polygons = new Dictionary<IOPrimitive, List<int[]>>();

                var enusculture = new CultureInfo("en-US");
                var prevCultureInfo = CultureInfo.CurrentCulture;

                CultureInfo.CurrentCulture = enusculture;

                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    line = line.Replace(",", ".");

                    var args = Regex.Replace(line.Trim(), @"\s+", " ").Split(' ');
                    
                    if (args.Length > 0)
                        switch (args[0])
                        {
                            case "v":
                                v.Add(new Vector3(
                                    args.Length > 1 ? float.Parse(args[1]) : 0,
                                    args.Length > 2 ? float.Parse(args[2]) : 0,
                                    args.Length > 3 ? float.Parse(args[3]) : 0));
                                break;
                            case "vt":
                                vt.Add(new Vector2(
                                    args.Length > 1 ? float.Parse(args[1]) : 0,
                                    args.Length > 2 ? float.Parse(args[2]) : 0));
                                break;
                            case "vn":
                                vn.Add(new Vector3(
                                    args.Length > 1 ? float.Parse(args[1]) : 0,
                                    args.Length > 2 ? float.Parse(args[2]) : 0,
                                    args.Length > 3 ? float.Parse(args[3]) : 0));
                                break;
                            case "f":
                                var faces = ParseFaces(args);

                                if (args.Length == 2)
                                {
                                    // point
                                    if (!polygons.ContainsKey(IOPrimitive.POINT))
                                        polygons.Add(IOPrimitive.POINT, new List<int[]>());
                                    polygons[IOPrimitive.POINT].AddRange(faces);
                                }
                                if (args.Length == 3)
                                {
                                    // line
                                    if (!polygons.ContainsKey(IOPrimitive.LINE))
                                        polygons.Add(IOPrimitive.LINE, new List<int[]>());
                                    polygons[IOPrimitive.LINE].AddRange(faces);
                                }
                                if (args.Length == 4)
                                {
                                    // triangle
                                    if (!polygons.ContainsKey(IOPrimitive.TRIANGLE))
                                        polygons.Add(IOPrimitive.TRIANGLE, new List<int[]>());
                                    polygons[IOPrimitive.TRIANGLE].AddRange(faces);
                                }
                                if (args.Length == 5)
                                {
                                    // quad
                                    if (!polygons.ContainsKey(IOPrimitive.QUAD))
                                        polygons.Add(IOPrimitive.QUAD, new List<int[]>());
                                    polygons[IOPrimitive.QUAD].AddRange(faces);
                                }
                                if (args.Length == 6)
                                {
                                    // strip
                                    if (!polygons.ContainsKey(IOPrimitive.TRISTRIP))
                                        polygons.Add(IOPrimitive.TRISTRIP, new List<int[]>());
                                    polygons[IOPrimitive.TRISTRIP].AddRange(faces);
                                }
                                break;
                            case "o":
                                if(polygons.Count > 0)
                                    objects.Add(new Tuple<string, string, Dictionary<IOPrimitive, List<int[]>>>(objName, matNam, polygons));
                                objName = args[1];
                                matNam = "";
                                polygons = new Dictionary<IOPrimitive, List<int[]>>();
                                break;
                            case "usemtl":
                                if (args.Length > 1)
                                    matNam = args[1];
                                break;
                        }
                }

                //Reset back
                CultureInfo.CurrentCulture = prevCultureInfo;

                objects.Add(new Tuple<string, string, Dictionary<IOPrimitive, List<int[]>>>(objName, matNam, polygons));

                // generate model
                IOModel model = new IOModel();
                scene.Models.Add(model);

                // dummy bone
                model.Skeleton.RootBones.Add(new Core.Skeleton.IOBone()
                {
                    Name = "Root"
                });

                // convert and add polygons
                foreach(var obj in objects)
                {
                    IOMesh mesh = new IOMesh()
                    {
                        Name = obj.Item1
                    };

                    foreach(var p in obj.Item3)
                    {
                        IOPolygon poly = new IOPolygon()
                        {
                            PrimitiveType = p.Key,
                            MaterialName = obj.Item2,
                        };

                        for(int i = 0; i < p.Value.Count; i++)
                        {
                            var face = p.Value[i];

                            //Attribute face index is based on the order they are written in
                            //Normal index shifts by 1 if using tex coords
                            int normalIndex = vt.Count > 0 ? 2 : 1; 

                            IOVertex vert = new IOVertex()
                            {
                                Position = face[0] != -1 ? v[face[0]] : Vector3.Zero,
                                Normal = face[normalIndex] != -1 ? vn[face[normalIndex]] : Vector3.Zero,
                            };
                            if(face[1] != -1)
                                vert.UVs.Add(vt[face[1]]);

                            poly.Indicies.Add(mesh.Vertices.Count);
                            mesh.Vertices.Add(vert);
                        }

                        mesh.Polygons.Add(poly);
                    }

                    // add mesh to model
                    model.Meshes.Add(mesh);
                };
            }
            
            return scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<int[]> ParseFaces(string[] args)
        {
            List<int[]> faces = new List<int[]>();
            for(int i = 1; i < args.Length; i++)
            {
                var f = args[i].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                int[] face = new int[3];
                for(int j = 0; j < 3; j++)
                {
                    if (j < f.Length)
                        face[j] = int.Parse(f[j]) - 1;
                    else
                        face[j] = -1;
                }
                faces.Add(face);
            }
            return faces;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="filePath"></param>
        private void LoadMaterialLibrary(IOScene scene, string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            using (StreamReader r = new StreamReader(stream))
            {
                IOMaterial currentMaterial = new IOMaterial();

                var enusculture = new CultureInfo("en-US");
                var prevCultureInfo = CultureInfo.CurrentCulture;
                CultureInfo.CurrentCulture = enusculture;

                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    line = line.Replace(",", ".");

                    var args = Regex.Replace(line.Trim(), @"\s+", " ").Split(' ');

                    if (args.Length == 0)
                        continue;

                    switch(args[0])
                    {
                        case "newmtl":
                            currentMaterial = new IOMaterial()
                            {
                                Name = args[1]
                            };
                            scene.Materials.Add(currentMaterial);
                            break;
                        case "Ka":
                            currentMaterial.AmbientColor = new Vector4(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), 1);
                            break;
                        case "Kd":
                            currentMaterial.DiffuseColor = new Vector4(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), 1);
                            break;
                        case "Ks":
                            currentMaterial.SpecularColor = new Vector4(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), 1);
                            break;
                        case "Ns":
                            currentMaterial.Shininess = float.Parse(args[1]);
                            break;
                        case "d":
                            currentMaterial.Alpha = float.Parse(args[1]);
                            break;
                        case "Tr":
                            currentMaterial.Alpha = 1 - float.Parse(args[1]);
                            break;
                        case "map_Ka":
                            currentMaterial.AmbientMap = new IOTexture()
                            {
                                Name = currentMaterial.Name + "_texture",
                                FilePath = string.Join(" ", args, 1, args.Length - 1)
                            };
                            break;
                        case "map_Kd":
                            currentMaterial.DiffuseMap = new IOTexture()
                            {
                                Name = currentMaterial.Name + "_texture",
                                FilePath = string.Join(" ", args, 1, args.Length - 1)
                            };
                            break;
                        case "map_Ks":
                            currentMaterial.SpecularMap = new IOTexture()
                            {
                                Name = currentMaterial.Name + "_texture",
                                FilePath = string.Join(" ", args, 1, args.Length - 1)
                            };
                            break;
                    }
                }

                //Reset back
                CultureInfo.CurrentCulture = prevCultureInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "Wavefront OBJ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Verify(string filePath)
        {
            return Path.GetExtension(filePath).ToLower().Equals(".obj");
        }
    }
}
