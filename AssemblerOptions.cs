namespace Assembler
{
    public class AssemblerOptions
    {
        public string? InputFile { get; set; }
        public string? OutputFile { get; set; }
        public int LoaderAddress { get; set; }

        public bool Verbose { get; set; } = false;
    }
}
