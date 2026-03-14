
namespace Assembler
{
    public static class OSXWriter
    {
        public static void Write(string outputPath, int byteSize, int PC, int loaderAddress, byte[] binary)
        {
            using FileStream stream = new(outputPath, FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(stream);

            writer.Write(byteSize);
            writer.Write(PC);
            writer.Write(loaderAddress);
            writer.Write(binary);
        }
    }
}