namespace Assembler
{
    public static class VerbosePrinter
    {
        public static void PrintStatements(List<Statement> statements)
        {
            Console.WriteLine("\nStatements:");

            int line = 0;

            foreach (var stmt in statements)
            {
                line++;
                Console.WriteLine($"[{line}] {FormatStatement(stmt)}");
            }
        }

        public static void PrintSymbolTable(Dictionary<string, int> table)
        {
            Console.WriteLine("\nSymbol Table:");

            foreach (var entry in table)
                Console.WriteLine($"{entry.Key} -> {entry.Value}");
        }


        public static void PrintListings(List<ListingEntry> listing, bool debug)
        {
            Console.WriteLine();
            Console.Write("ADDR   BYTES                      LABEL    OPCODE   OPERANDS");
            if (debug)
                Console.Write("   TOKENS");
            Console.WriteLine();
                
            foreach (var entry in listing)
            {
                var addr = entry.Address.ToString();
                var bytes = entry.Bytes.Length <= 6 ? string.Join(" ", entry.Bytes.Select(b => $"{b,3}")) : string.Join(" ", entry.Bytes[..5].Select(b => $"{b,3}").Append("..."));
                var stmt = FormatStatement(entry.Statement);
                var tokens = debug ? string.Join(" ", entry.Tokens.Select(t => $"{t.Type,-10}")) : "";

                Console.WriteLine($"{addr,4}   {bytes,-26} {stmt,-26} {tokens}");
            }
        }

        private static string FormatStatement(Statement s)
        {
            var parts = new List<string>();
            
            parts.Add($"{s.label,-8}");

            if (s.opCode != null)
                parts.Add($"{s.opCode,-8}");

            if (s.directive != null)
                parts.Add($"{"."+s.directive,-8}");

            if (s.operands.Length > 0)
                parts.Add($"{string.Join(" ", s.operands.Select(o => o.Text)),-10}");
            else
                parts.Add($"{"",-10}");

            return string.Join(" ", parts);
        }
        
    }
}