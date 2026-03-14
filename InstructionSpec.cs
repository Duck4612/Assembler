namespace Assembler
{
    public record InstructionSpec(byte Opcode, Token.Types[] Operands)
    {
        public byte Opcode { get; } = Opcode;
        public Token.Types[] Operands { get; } = Operands;
    }
}