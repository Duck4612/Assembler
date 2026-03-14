namespace Assembler
{
    public record InstructionSpec(byte Opcode, OperandFormat OperandFormat)
    {
        public byte Opcode { get; } = Opcode;
        public OperandFormat OperandFormat { get; } = OperandFormat;
    }
}