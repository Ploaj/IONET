using System;
using System.Collections.Generic;
using System.IO;
using IONET.Core;

namespace IONET.MayaAnim
{
    class MayaAnimExporter : ISceneExporter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void ExportScene(IOScene scene, string filePath, ExportSettings settings)
        {
            if (scene.Models.Count == 0)
                throw new Exception($"Failed to export animation! Model must have a skeleton to export!");

            MayaAnim.ExportAnimation(filePath, settings, 
                scene.Animations[0], 
                scene.Models[0].Skeleton);
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
    }
}
