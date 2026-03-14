namespace Assembler
{
    public class InstructionConfig
    {
        public string Name { get; set; } = "";
        public byte Opcode { get; set; }
        public Token.Types[] Operands { get; set; } = [];
    }
}