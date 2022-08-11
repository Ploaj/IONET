using IONET.Core;
using System.IO;

namespace IONET.MIO
{
    public class MIOExporter : ISceneExporter
    {
        public void ExportScene(IOScene scene, string filePath, ExportSettings settings)
        {
            using (FileStream s = new FileStream(filePath, FileMode.Create))
            using (BinaryWriter w = new BinaryWriter(s))
            {
                // write header
                w.Write("MIO1".ToCharArray());
                w.Write(scene.Models.Count);
                w.Write(scene.Materials.Count);
                w.Write(scene.Name);

                foreach(var m in scene.Models)
                {
                    w.Write(m.Name);
                    w.Write(m.Skeleton.BreathFirstOrder().Count);
                    w.Write(m.Meshes.Count);

                    foreach(var b in m.Skeleton.BreathFirstOrder())
                    {
                        w.Write(b.Name);
                        w.Write(m.Skeleton.IndexOf(b.Parent));
                        w.Write(b.TranslationX);
                        w.Write(b.TranslationY);
                        w.Write(b.TranslationZ);
                        w.Write(b.Rotation.X);
                        w.Write(b.Rotation.Y);
                        w.Write(b.Rotation.Z);
                        w.Write(b.Rotation.W);
                        w.Write(b.ScaleX);
                        w.Write(b.ScaleY);
                        w.Write(b.ScaleZ);
                    }
                }
            }
        }

        public string[] GetExtensions()
        {
            return new string[] { ".mio" };
        }

        public string Name()
        {
            return "Model IO";
        }
    }
}
