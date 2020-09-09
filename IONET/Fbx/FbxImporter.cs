using IONET.Core;
using IONET.Core.Model;
using System;
using System.IO;

namespace IONET.Fbx
{
    public class FbxImporter : IModelLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IOScene GetScene(string filePath)
        {
            FbxHelper helper = new FbxHelper(FbxIO.ReadBinary(filePath));

            if (helper.Version != 7400 && helper.Version != 6100 && helper.Version != 7500)
                throw new NotSupportedException($"FBX Version {helper.Version} not supported");

            IOScene scene = new IOScene();

            IOModel model = new IOModel();
            model.Skeleton = helper.GetSkeleton();
            scene.Models.Add(model);

            model.Meshes.AddRange(helper.ExtractMesh());
            
            scene.Materials.AddRange(helper.GetMaterials());

            return scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetExtensions()
        {
            return new string[] { ".fbx" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "Autodesk FBX";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Verify(string filePath)
        {
            return Path.GetExtension(filePath).ToLower().Equals(".fbx");
        }
    }
}
