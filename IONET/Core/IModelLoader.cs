namespace IONET.Core
{
    public interface IModelLoader
    {
        string Name();

        string[] GetExtensions();

        bool Verify(string filePath);

        IOScene GetScene(string filePath);
    }
}
