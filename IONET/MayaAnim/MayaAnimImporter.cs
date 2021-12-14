using System;
using System.Collections.Generic;
using System.IO;
using IONET.Core;

namespace IONET.MayaAnim
{
    class MayaAnimImporter : ISceneLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IOScene GetScene(string filePath)
        {
            IOScene scene = new IOScene();
            scene.Animations.Add(MayaAnim.ImportAnimation(filePath, new ImportSettings()));
            return scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetExtensions() => new string[] { ".anim" };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name() => "Autodesk Maya Anim";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Verify(string filePath) {
            return Path.GetExtension(filePath).ToLower().Equals(".anim");
        }
    }
}
