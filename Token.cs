
namespace Assembler
{
    public class Token(Token.Types type, string text, int line)
    {
        public enum Types
        {
            Directive,
            OPCode,
            Number,
            Register,
            Label
        }

        public static readonly Dictionary<Types, int> Sizes = new()
        {
            [Types.Directive] = 0,
            [Types.OPCode] = 1,
            [Types.Number] = 4,
            [Types.Register] = 1,
            [Types.Label] = 4
        };

        public Types Type { get; } = type;
        public string Text { get; } = text;
        public int Line { get; } = line;
    }
}
