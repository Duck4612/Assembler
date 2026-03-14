namespace Assembler
{
    public static class InstructionSet
    {
        public static readonly Dictionary<string, InstructionSpec> Specs = new(StringComparer.OrdinalIgnoreCase);

        public static readonly Dictionary<string, InstructionSpec> Default =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["ADR"] = new InstructionSpec(Opcode: 0, OperandFormat: OperandFormat.RLabel),
                ["MOV"] = new InstructionSpec(Opcode: 1, OperandFormat: OperandFormat.RR),
                ["STR"] = new InstructionSpec(Opcode: 2, OperandFormat: OperandFormat.RR),
                ["STRB"] = new InstructionSpec(Opcode: 3, OperandFormat: OperandFormat.RR),
                ["LDR"] = new InstructionSpec(Opcode:  4, OperandFormat: OperandFormat.RR),
                ["LDRB"] = new InstructionSpec(Opcode: 5, OperandFormat: OperandFormat.RR),
                ["BX"] = new InstructionSpec(Opcode: 6, OperandFormat: OperandFormat.R),
                ["B"] = new InstructionSpec(Opcode: 7, OperandFormat: OperandFormat.Label),
                ["BNE"] = new InstructionSpec(Opcode: 8, OperandFormat: OperandFormat.Label),
                ["BGT"] = new InstructionSpec(Opcode: 9, OperandFormat: OperandFormat.Label),
                ["BLT"] = new InstructionSpec(Opcode: 10, OperandFormat: OperandFormat.Label),
                ["BEQ"] = new InstructionSpec(Opcode: 11, OperandFormat: OperandFormat.Label),
                ["CMP"] = new InstructionSpec(Opcode: 12, OperandFormat: OperandFormat.RR),
                ["AND"] = new InstructionSpec(Opcode: 13, OperandFormat: OperandFormat.RR),
                ["ORR"] = new InstructionSpec(Opcode: 14, OperandFormat: OperandFormat.RR),
                ["EOR"] = new InstructionSpec(Opcode: 15, OperandFormat: OperandFormat.RR),
                ["ADD"] = new InstructionSpec(Opcode: 16, OperandFormat: OperandFormat.RRR),
                ["SUB"] = new InstructionSpec(Opcode: 17, OperandFormat: OperandFormat.RRR),
                ["MUL"] = new InstructionSpec(Opcode: 18, OperandFormat: OperandFormat.RRR),
                ["DIV"] = new InstructionSpec(Opcode: 19, OperandFormat: OperandFormat.RRR),
                ["SWI"] = new InstructionSpec(Opcode: 20, OperandFormat: OperandFormat.I),
                ["BL"] = new InstructionSpec(Opcode: 21, OperandFormat: OperandFormat.Label),
                ["MVI"] = new InstructionSpec(Opcode: 22, OperandFormat: OperandFormat.RI),
            };

        public static void SetSpecsToDefault()
        {
            Specs.Clear();
            foreach (var kvp in Default)
            {
                Specs[kvp.Key] = kvp.Value;
            }
        }
    }
}