using IONET.Core;
using System.Collections.Generic;
using System.IO;

namespace IONET.Wavefront
{
    public class OBJExporter : IModelExporter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="filePath"></param>
        public void ExportScene(IOScene scene, string filePath, ExportSettings settings)
        {
            var matLib = filePath.Replace(".obj", ".mtl");

            // sanatize material names
            scene.CleanMaterialNames();
            ExportMTL(scene, matLib);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            using (StreamWriter w = new StreamWriter(stream))
            {
                w.WriteLine($"mtllib {Path.GetFileName(matLib)}");

                Dictionary<string, int> v_strToIndex = new Dictionary<string, int>();
                Dictionary<string, int> vt_strToIndex = new Dictionary<string, int>();
                Dictionary<string, int> vn_strToIndex = new Dictionary<string, int>();

                foreach (var mod in scene.Models)
                {
                    foreach(var mesh in mod.Meshes)
                    {
                        // make mesh triangles
                        mesh.MakeTriangles();

                        // 
                        Dictionary<int, int> vmap = new Dictionary<int, int>();
                        Dictionary<int, int> vtmap = new Dictionary<int, int>();
                        Dictionary<int, int> vnmap = new Dictionary<int, int>();

                        // add vertices
                        for (int i = 0; i < mesh.Vertices.Count; i++)
                        {
                            var v = mesh.Vertices[i];
                            
                            // positions
                            var vv = $"v {v.Position.X} {v.Position.Y} {v.Position.Z}";
                            if (!v_strToIndex.ContainsKey(vv))
                            {
                                v_strToIndex.Add(vv, v_strToIndex.Count);
                                w.WriteLine(vv);
                            }
                            vmap.Add(i, v_strToIndex[vv] + 1);

                            // uvs
                            if (v.UVs.Count > 0)
                            {
                                var vtv = $"vt {v.UVs[0].X} {v.UVs[0].Y}";

                                if (!vt_strToIndex.ContainsKey(vtv))
                                {
                                    vt_strToIndex.Add(vtv, vt_strToIndex.Count);
                                    w.WriteLine(vtv);
                                }
                                vtmap.Add(i, vt_strToIndex[vtv] + 1);
                            }

                            // normals
                            var vnv = $"vn {v.Normal.X} {v.Normal.Y} {v.Normal.Z}";
                            if (!vn_strToIndex.ContainsKey(vnv))
                            {
                                vn_strToIndex.Add(vnv, vn_strToIndex.Count);
                                w.WriteLine(vnv);
                            }
                            vnmap.Add(i, vn_strToIndex[vnv] + 1);
                        }

                        // add polygons
                        w.WriteLine($"o {mesh.Name}");
                        foreach (var poly in mesh.Polygons)
                        {
                            w.WriteLine($"g polygon_{mesh.Polygons.IndexOf(poly)}");

                            w.WriteLine($"usemtl {poly.MaterialName}");
                            
                            for(int i = 0; i < poly.Indicies.Count; i+=3)
                            {
                                w.WriteLine(
                                    $"f {vmap[poly.Indicies[i]]}/{(vtmap.ContainsKey(poly.Indicies[i]) ? vtmap[poly.Indicies[i]].ToString() : "")}/{vnmap[poly.Indicies[i]]} " +
                                    $"{vmap[poly.Indicies[i + 1]]}/{(vtmap.ContainsKey(poly.Indicies[i + 1]) ? vtmap[poly.Indicies[i + 1]].ToString() : "")}/{vnmap[poly.Indicies[i + 1]]} " +
                                    $"{vmap[poly.Indicies[i + 2]]}/{(vtmap.ContainsKey(poly.Indicies[i + 2]) ? vtmap[poly.Indicies[i + 2]].ToString() : "")}/{vnmap[poly.Indicies[i + 2]]}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="filePath"></param>
        private static void ExportMTL(IOScene scene, string filePath)
        {
            using (FileStream stream = File.OpenWrite(filePath))
            using (StreamWriter w = new StreamWriter(stream))
            {
                foreach(var mat in scene.Materials)
                {
                    w.WriteLine($"newmtl {mat.Name}");

                    w.WriteLine(" illum 2");
                    w.WriteLine($" Ka {mat.AmbientColor.X} {mat.AmbientColor.Y} {mat.AmbientColor.Z}");
                    w.WriteLine($" Kd {mat.DiffuseColor.X} {mat.DiffuseColor.Y} {mat.DiffuseColor.Z}");
                    w.WriteLine($" Ks {mat.SpecularColor.X} {mat.SpecularColor.Y} {mat.SpecularColor.Z}");
                    w.WriteLine($" Ns {mat.Shininess}");
                    w.WriteLine($" d {mat.Alpha}");
                    w.WriteLine($" Tr {1 - mat.Alpha}");

                    if (mat.AmbientMap != null) w.WriteLine($" map_Ka {mat.AmbientMap.FilePath}");
                    if (mat.DiffuseMap != null) w.WriteLine($" map_Kd {mat.DiffuseMap.FilePath}");
                    if (mat.SpecularMap != null) w.WriteLine($" map_Ks {mat.SpecularMap.FilePath}");
                }
            }
        }

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
        /// <returns></returns>
        public string Name()
        {
            return "Wavefront OBJ";
        }
    }
}
