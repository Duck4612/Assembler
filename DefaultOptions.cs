namespace Assembler
{
    public static class DefaultOptions
    {
        public static AssemblerOptions Create() => new()
        {
            InputFile = "",
            OutputFile = "",

            Verbose = false,
            Debug = false,
            LoaderAddress = 0
        };
    }
}