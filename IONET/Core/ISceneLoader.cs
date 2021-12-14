namespace IONET.Core
{
    public interface ISceneLoader
    {
        string Name();

        string[] GetExtensions();

        bool Verify(string filePath);

        IOScene GetScene(string filePath);
    }
}
