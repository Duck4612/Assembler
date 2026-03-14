namespace Assembler
{
    public static class OperandSet
    {
        public static readonly Dictionary<OperandFormat, OperandSpec[]> Specs = new()
        {
            [OperandFormat.R]      = [new OperandSpec(TokenType.Register, 1, 1)],
            [OperandFormat.RR]     = [new OperandSpec(TokenType.Register, 1, 1), new OperandSpec(TokenType.Register, 2, 1)],
            [OperandFormat.RRR]    = [new OperandSpec(TokenType.Register, 1, 1), new OperandSpec(TokenType.Register, 2, 1), new OperandSpec(TokenType.Register, 3, 1)],
            [OperandFormat.RI]     = [new OperandSpec(TokenType.Register, 1, 1), new OperandSpec(TokenType.Number,   2, 4)],
            [OperandFormat.RLabel] = [new OperandSpec(TokenType.Register, 1, 1), new OperandSpec(TokenType.Label,    2, 4)],
            [OperandFormat.Label]  = [new OperandSpec(TokenType.Label,    1, 4)],
            [OperandFormat.I]      = [new OperandSpec(TokenType.Number,   1, 4)]
        };
    }
}