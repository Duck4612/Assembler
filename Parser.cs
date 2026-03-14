namespace Assembler
{
	public class Parser
	{
        public static List<Statement> Parse(List<List<Token>> tokenLines)
        {
            var statements = new List<Statement>();
            bool enteredCodeSection = false;

            for(int lineNumber = 0; lineNumber < tokenLines.Count; lineNumber++)
            {
                var tokenLine = tokenLines[lineNumber];
                
                if (tokenLine.Count == 0)
                    continue;

                var lineIndex = 0;
                string? label = null;
                string? opCode = null;
                string? directive = null;
                Token[] sourceTokens = [];

                while (opCode == null && directive == null && lineIndex < tokenLine.Count)
                {
                    switch (tokenLine[lineIndex].Type)
                    {
                        case TokenType.Label when label == null:
                            sourceTokens = [.. sourceTokens, tokenLine[lineIndex]];
                            label = tokenLine[lineIndex++].Text;
                            tokenLine = lineIndex >= tokenLine.Count ? tokenLines[++lineNumber] : tokenLine;
                            lineIndex = lineIndex >= tokenLines[sourceTokens[0].Line].Count ? lineIndex - 1 : lineIndex;
                            break;
                        case TokenType.Label when label != null:
                            throw new Exception($"Multiple labels on line {lineNumber + 1} before OPCode or Directive (current label {label} | overloading label {tokenLine[lineIndex].Text})");
                        case TokenType.OPCode:
                            sourceTokens = [.. sourceTokens, tokenLine[lineIndex]];
                            opCode = tokenLine[lineIndex++].Text;
                            enteredCodeSection = true;
                            break;
                        case TokenType.Directive when !enteredCodeSection:
                            sourceTokens = [.. sourceTokens, tokenLine[lineIndex]];
                            directive = tokenLine[lineIndex++].Text;
                            break;
                        case TokenType.Directive when enteredCodeSection:
                            throw new Exception($"Directive '{tokenLine[lineIndex].Text}' cannot appear after code section has started on line {lineNumber + 1}");
                        default:
                            throw new Exception($"Unexpected token '{tokenLine[lineIndex].Text}' of type {tokenLine[lineIndex].Type} on line {lineNumber + 1} (expected OPCode or Directive before operands)");
                    }
                }

                var operands = tokenLine.Skip(lineIndex).ToArray();
                sourceTokens = [.. sourceTokens, .. operands];
                if (opCode != null)
                {
                    if (!InstructionSet.Specs.TryGetValue(opCode, out var spec))
                        throw new Exception($"Unknown opcode {opCode}");

                    var operandSpecs = OperandSet.Specs[spec.OperandFormat];

                    if (operands.Length != operandSpecs.Length)
                        throw new Exception($"Invalid operand set length for {opCode}: Expected {operandSpecs.Length} | Actual {operands.Length}");

                    for (int i = 0; i < operands.Length; i++) 
                    {
                        if (operands[i].Type != operandSpecs[i].Type)
                            throw new Exception($"Invalid operand types for {opCode}: Expected {operandSpecs[i].Type} | Actual {operands[i].Type} ({operands[i].Text})");
                    }
                }

                statements.Add(new Statement(label, opCode, directive, operands, sourceTokens));
            }

            return statements;
        }
    }
}