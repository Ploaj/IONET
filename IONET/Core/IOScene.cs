using IONET.Core.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IONET.Core
{
    public class IOScene
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "Scene";

        /// <summary>
        /// 
        /// </summary>
        public List<IOModel> Models { get; internal set; } = new List<IOModel>();
        
        /// <summary>
        /// 
        /// </summary>
        public List<IOMaterial> Materials { get; internal set; } = new List<IOMaterial>();

        /// <summary>
        /// Cleans material names to allow for smoother export
        /// </summary>
        public void CleanMaterialNames()
        {
            Dictionary<string, string> nameToName = new Dictionary<string, string>();

            foreach(var mat in Materials)
            {
                var sanatize = Regex.Replace(mat.Name.Trim(), @"\s+", "_").Replace("#", "");
                nameToName.Add(mat.Name, sanatize);
                mat.Name = sanatize;
            }

            foreach (var mod in Models)
                foreach (var mesh in mod.Meshes)
                    foreach (var poly in mesh.Polygons)
                        if (nameToName.ContainsKey(poly.MaterialName))
                            poly.MaterialName = nameToName[poly.MaterialName];
        }
    }
}
