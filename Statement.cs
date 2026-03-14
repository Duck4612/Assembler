namespace Assembler
{
    public class Statement(string? label, string? opCode, string? directive, Token[] operands, Token[] sourceTokens)
	{
        public string? label = label;
        public string? opCode = opCode;
        public string? directive = directive;
		public Token[] operands = operands;
        public Token[] sourceTokens = sourceTokens;
    }
}