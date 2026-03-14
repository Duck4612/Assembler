namespace Assembler
{
    public class Directive
    {
        public static readonly HashSet<string> Codes = new(StringComparer.OrdinalIgnoreCase) { "WORD", "BYTE", "SPACE", "INPUT", "STRING" };
    }
}