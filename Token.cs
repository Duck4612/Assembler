
namespace Assembler
{
    public class Token(TokenType type, string text, int line)
    {
        public TokenType Type { get; } = type;
        public string Text { get; } = text;
        public int Line { get; } = line;
    }
}
