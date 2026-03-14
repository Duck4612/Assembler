namespace Assembler
{
    public static class InstructionSet
    {
        public static readonly Dictionary<string, InstructionSpec> Specs = new(StringComparer.OrdinalIgnoreCase)
            {
                ["ADR"]  = new InstructionSpec(Opcode: 0,  Operands: [Token.Types.Register, Token.Types.Label]),
                ["MOV"]  = new InstructionSpec(Opcode: 1,  Operands: [Token.Types.Register, Token.Types.Register]),
                ["STR"]  = new InstructionSpec(Opcode: 2,  Operands: [Token.Types.Register, Token.Types.Register]),
                ["STRB"] = new InstructionSpec(Opcode: 3,  Operands: [Token.Types.Register, Token.Types.Register]),
                ["LDR"]  = new InstructionSpec(Opcode: 4,  Operands: [Token.Types.Register, Token.Types.Register]),
                ["LDRB"] = new InstructionSpec(Opcode: 5,  Operands: [Token.Types.Register, Token.Types.Register]),
                ["BX"]   = new InstructionSpec(Opcode: 6,  Operands: [Token.Types.Register]),
                ["B"]    = new InstructionSpec(Opcode: 7,  Operands: [Token.Types.Label]),
                ["BNE"]  = new InstructionSpec(Opcode: 8,  Operands: [Token.Types.Label]),
                ["BGT"]  = new InstructionSpec(Opcode: 9,  Operands: [Token.Types.Label]),
                ["BLT"]  = new InstructionSpec(Opcode: 10, Operands: [Token.Types.Label]),
                ["BEQ"]  = new InstructionSpec(Opcode: 11, Operands: [Token.Types.Label]),
                ["CMP"]  = new InstructionSpec(Opcode: 12, Operands: [Token.Types.Register, Token.Types.Register]),
                ["AND"]  = new InstructionSpec(Opcode: 13, Operands: [Token.Types.Register, Token.Types.Register]),
                ["ORR"]  = new InstructionSpec(Opcode: 14, Operands: [Token.Types.Register, Token.Types.Register]),
                ["EOR"]  = new InstructionSpec(Opcode: 15, Operands: [Token.Types.Register, Token.Types.Register]),
                ["ADD"]  = new InstructionSpec(Opcode: 16, Operands: [Token.Types.Register, Token.Types.Register, Token.Types.Register]),
                ["SUB"]  = new InstructionSpec(Opcode: 17, Operands: [Token.Types.Register, Token.Types.Register, Token.Types.Register]),
                ["MUL"]  = new InstructionSpec(Opcode: 18, Operands: [Token.Types.Register, Token.Types.Register, Token.Types.Register]),
                ["DIV"]  = new InstructionSpec(Opcode: 19, Operands: [Token.Types.Register, Token.Types.Register, Token.Types.Register]),
                ["SWI"]  = new InstructionSpec(Opcode: 20, Operands: [Token.Types.Number]),
                ["BL"]   = new InstructionSpec(Opcode: 21, Operands: [Token.Types.Label]),
                ["MVI"]  = new InstructionSpec(Opcode: 22, Operands: [Token.Types.Register, Token.Types.Number])
            };
    }
}