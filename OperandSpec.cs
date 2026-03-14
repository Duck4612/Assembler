namespace Assembler
{
    public class OperandSpec(TokenType Type, int Offset, int Size)
    {
        public TokenType Type { get; } = Type;
        public int Offset { get; } = Offset;
        public int Size { get; } = Size;
    }
}