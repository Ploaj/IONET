namespace IONET.Core.Model
{
    public enum WrapMode
    {
        REPEAT,
        CLAMP,
        MIRROR
    }

    /// <summary>
    /// 
    /// </summary>
    public class IOTexture
    {
        public string Name = "";

        public string FilePath = "";

        public int UVChannel = 0;

        public WrapMode WrapS = WrapMode.REPEAT;

        public WrapMode WrapT = WrapMode.REPEAT;
    }
}
