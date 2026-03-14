using System.Text.Json;

namespace Assembler
{
    internal class Program
    {
        private static readonly string ParamFileName = "AsmOptions.json";
        private static readonly string ISAFileName = "isa.json";
        private static readonly string Help = "Usage: <input.asm> <loaderAddress> [-v] [-o <output.osx>]";
        static void Main(string[] args)
        {
            if (args.Contains("-h"))
            {
                Console.WriteLine(Help);
                return;
            }
            var options = GetAssemblerOptions(args);
            
            Console.WriteLine($"Beginning assembly: Input file - {options.InputFile} | Output file - {options.OutputFile} | Loader Address - {options.LoaderAddress} ");
            List<ListingEntry> listing = [];

            ISAConfigLoader.Load(ISAFileName);

            var lines = AsmLoader.ReadLines(options.InputFile!);

            var tokens = Lexer.Tokenize(lines);
            var statements = Parser.Parse(tokens);
            var symbolTable = Assembler.FirstPass(statements, out int PC, out int byteSize);
            var binary = Assembler.SecondPass(statements, symbolTable, listing);
            OSXWriter.Write(options.OutputFile!, byteSize, PC, options.LoaderAddress, binary);
            Console.WriteLine($"Finished assembly: Input file - {options.InputFile} | Output file - {options.OutputFile} | ByteSize - {byteSize} | PC - {PC} | LoaderAddress - {options.LoaderAddress}");

            if (options.Verbose)
            {
                VerbosePrinter.PrintListings(listing, args.Contains("--d"));
            }

            if (args.Contains("--d"))
            {
                VerbosePrinter.PrintSymbolTable(symbolTable);
            }
        }

        public static AssemblerOptions ParamFileOptions(string path)
        {
            if (!File.Exists(path))
                return new AssemblerOptions();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AssemblerOptions>(json) ?? new AssemblerOptions();
        } 

        public static AssemblerOptions GetAssemblerOptions(string[] args)
        {
            var config = ParamFileOptions(ParamFileName);
            var cli = new AssemblerOptions
            {
                InputFile = args.FirstOrDefault(arg => arg.EndsWith(".asm")),
                OutputFile = args.Contains("-o") ? Path.ChangeExtension(args[Array.IndexOf(args, "-o") + 1], ".osx") : Path.ChangeExtension(args.FirstOrDefault(arg => arg.EndsWith(".asm")), ".osx"),
                LoaderAddress = args.Length > 1 ? (int.TryParse(args[1], out int address) ? address : -1) : -1,
                Verbose = args.Contains("-v")
            };

            var options = new AssemblerOptions
            {
                InputFile = cli.InputFile ?? config.InputFile ?? throw new Exception("Assembly requires a target .asm input file"),
                OutputFile = cli.OutputFile ?? config.OutputFile ?? throw new Exception("Assembly requires a target .osx output file"),
                LoaderAddress = cli.LoaderAddress != -1 ? cli.LoaderAddress :
                        config.LoaderAddress != -1 ? config.LoaderAddress :
                        throw new Exception("Assembly requires a target base loading address"),

                Verbose = cli.Verbose || ((cli.InputFile != null) && config.Verbose),
            };

            File.WriteAllText(ParamFileName, JsonSerializer.Serialize(options, new JsonSerializerOptions { WriteIndented = true }));
            return options;
        }
    }
}