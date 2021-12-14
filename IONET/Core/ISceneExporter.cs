namespace IONET.Core
{
    public interface ISceneExporter
    {
        string Name();

        string[] GetExtensions();

        void ExportScene(IOScene scene, string filePath, ExportSettings settings);
    }
}
