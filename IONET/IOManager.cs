using IONET.Collada;
using IONET.Core;
using IONET.Fbx;
using IONET.SMD;
using IONET.Wavefront;
using System.IO;
using System.Linq;
using System.Text;

namespace IONET
{
    public class IOManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static IModelLoader[] ModelLoaders = new
            IModelLoader[]
        {
            new ColladaImporter(),
            new SMDImporter(),
            new OBJImporter(),
            new FbxImporter()
        };

        /// <summary>
        /// 
        /// </summary>
        private static IModelExporter[] ModelExporters = new
            IModelExporter[]
        {
            new ColladaExporter(),
            new SMDExporter(),
            new OBJExporter()
        };

        /// <summary>
        /// Gets a file filter for the export formats
        /// </summary>
        /// <returns></returns>
        public static string GetModelExportFileFilter()
        {
            StringBuilder sb = new StringBuilder();

            var allExt = string.Join(";*", ModelExporters.SelectMany(e => e.GetExtensions()));

            sb.Append($"Supported Files (*{allExt})|*{allExt}");

            foreach(var l in ModelExporters)
            {
                var ext = string.Join(";*", l.GetExtensions());
                sb.Append($"|{l.Name()} (*{ext})|*{ext}");
            }

            sb.Append("|All files (*.*)|*.*");

            return sb.ToString();
        }

        /// <summary>
        /// Gets a file filter for the import formats
        /// </summary>
        /// <returns></returns>
        public static string GetModelImportFileFilter()
        {
            StringBuilder sb = new StringBuilder();

            var allExt = string.Join(";*", ModelLoaders.SelectMany(e => e.GetExtensions()));

            sb.Append($"Supported Files (*{allExt})|*{allExt}");

            foreach (var l in ModelLoaders)
            {
                var ext = string.Join(";*", l.GetExtensions());
                sb.Append($"|{l.Name()} (*{ext})|*{ext}");
            }

            sb.Append("|All files (*.*)|*.*");

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IOScene LoadScene(string filePath, ImportSettings settings)
        {
            foreach(var l in ModelLoaders)
                if (l.Verify(filePath))
                {
                    var scene = l.GetScene(filePath);

                    // apply post processing
                    foreach (var model in scene.Models)
                        foreach (var m in model.Meshes)
                        {
                            // optimize vertices
                            if(settings.Optimize)
                                m.Optimize();

                            // make triangles
                            if (settings.Triangulate)
                                m.MakeTriangles();

                            // vertex modifications
                            if (settings.WeightLimit || settings.FlipUVs)
                                foreach (var v in m.Vertices)
                                {
                                    // weight limit
                                    if (settings.WeightLimit)
                                        v.Envelope.Optimize(settings.WeightLimitAmt);

                                    // flip uvs
                                    if (settings.FlipUVs)
                                        for (int i = 0; i < v.UVs.Count; i++)
                                            v.UVs[i] = new System.Numerics.Vector2(v.UVs[i].X, 1 - v.UVs[i].Y);
                                }

                            // flip winding order
                            if (settings.FlipWindingOrder)
                            {
                                foreach(var p in m.Polygons)
                                {
                                    p.ToTriangles(m);

                                    if(p.PrimitiveType == Core.Model.IOPrimitive.TRIANGLE)
                                    {
                                        for(int i = 0; i < p.Indicies.Count; i += 3)
                                        {
                                            var temp = p.Indicies[i + 1];
                                            p.Indicies[i + 1] = p.Indicies[i + 2];
                                            p.Indicies[i + 2] = temp;
                                        }
                                    }
                                }
                            }

                            // smooth normals
                            if (settings.SmoothNormals)
                                model.SmoothNormals();

                            // reset envelopes
                            foreach (var v in m.Vertices)
                                v.ResetEnvelope(model.Skeleton);
                        }

                    return scene;
                }

            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="filePath"></param>
        public static void ExportScene(IOScene scene, string filePath)
        {
            var ext = Path.GetExtension(filePath).ToLower();

            foreach (var l in ModelExporters)
                foreach (var e in l.GetExtensions())
                    if (e.Equals(ext))
                    {
                        l.ExportScene(scene, filePath);
                        break;
                    }
        }
        
    }
}
