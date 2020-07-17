namespace IONET.Core
{
    public interface IModelExporter
    {
        string Name();

        string[] GetExtensions();

        void ExportScene(IOScene scene, string filePath, ExportSettings settings);
    }
}
