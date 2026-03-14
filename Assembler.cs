namespace Assembler
{
    public class Assembler
    {
        private const int InstructionSize = 6;
        public static Dictionary<string, int> FirstPass(List<Statement> statements, out int PC, out int byteSize)
        {
            var symbolTable = new Dictionary<string, int>();
            int labelAddress = 0;
            PC = 0;
            byteSize = 0;
            bool seenInstruction = false;

            for (int i = 0; i < statements.Count; i++)
            {
                var statement = statements[i];
                if (statement.label != null)
                    symbolTable[statement.label] = labelAddress;

                switch (statement.directive)
                {
                    case "WORD":
                        labelAddress += 4;
                        // Reserve 4 bytes
                        break;
                    case "BYTE":
                        labelAddress += 1;
                        // Reserve 1 byte
                        break;
                    case "SPACE":
                        labelAddress += int.Parse(statement.operands[0].Text);
                        // Reserve specified number of bytes
                        break;
                    case "INPUT":
                        labelAddress += 4;
                        break;
                    case null:
                        if (statement.opCode == null)
                            break;
                        if (!seenInstruction)
                        {
                            PC = labelAddress;
                            seenInstruction = true;
                        }
                        labelAddress += InstructionSize;
                        break;
                    default:
                        throw new Exception($"Unknown directive {statement.directive}");
                }
            }

            byteSize = labelAddress;
            return symbolTable;
        }

        public static byte[] SecondPass(List<Statement> statements, Dictionary<string, int> symbolTable, List<ListingEntry> listing)
        {
            var binary = new List<byte>();
            var address = 0;

            for (int i = 0; i < statements.Count; i++)
            {
                var statement = statements[i];
                byte[]? byteEncoding = null;

                switch (statement.directive)
                {
                    case "WORD":
                        byteEncoding = statement.operands.Length > 0 ? BitConverter.GetBytes(int.Parse(statement.operands[0].Text)): new byte[4];
                        break;
                    case "BYTE":
                        byteEncoding = statement.operands.Length > 0 ? [byte.Parse(statement.operands[0].Text)] : new byte[1];
                        break;
                    case "SPACE":
                        byteEncoding = statement.operands.Length > 0 ? new byte[int.Parse(statement.operands[0].Text)] : throw new Exception($"Directive {statement.directive} requires size to allocate");
                        break;
                    case "INPUT":
                        int input;
                        Console.Write("Enter a number for .INPUT directive: ");
                        var userInput = Console.ReadLine();
                        
                        while (!int.TryParse(userInput, out input))
                        {
                            if (userInput == "")
                            {
                                input = 0;
                                break;
                            }
                            Console.Write("Enter a number for .INPUT directive: ");
                            userInput = Console.ReadLine();
                        }
                        byteEncoding = BitConverter.GetBytes(input);
                        statement.operands = [.. statement.operands, new Token(TokenType.Number, $"{input}", 0)];
                        break;
                }

                if (statement.opCode != null)
                    byteEncoding = EncodeInstruction(statement.opCode, statement.operands, symbolTable);

                if (byteEncoding == null)
                    throw new Exception("Statement led to a null byte encoding");

                binary.AddRange(byteEncoding);
                listing.Add(new ListingEntry
                {
                    Address = address,
                    Bytes = byteEncoding,
                    Statement = statement,
                    Tokens = statement.sourceTokens
                });

                address += byteEncoding.Length;
            }

            return [.. binary];
        }

        private static byte[] EncodeInstruction(string opCode, Token[] operands, Dictionary<string, int> symbols)
        {
            var spec = InstructionSet.Specs[opCode];
            var operandSpecs = OperandSet.Specs[spec.OperandFormat];
            byte[] bytes = new byte[6];

            bytes[0] = spec.Opcode;

            for (int i = 0; i < operandSpecs.Length; i++)
            {
                var operandSpec = operandSpecs[i];
                var token = operands[i];

                byte[] valueBytes = operandSpec.Type switch
                {
                    TokenType.Register => [Register.Codes[token.Text]],
                    TokenType.Number => BitConverter.GetBytes(int.Parse(token.Text)),
                    TokenType.Label => symbols.ContainsKey(token.Text) ? BitConverter.GetBytes(symbols[token.Text]) : throw new Exception($"Cannot find address of {token.Text}"),
                    _ => throw new Exception($"Unsupported operand type {token.Type}")
                };

                Array.Copy(valueBytes, 0, bytes, operandSpec.Offset, operandSpec.Size);
            }

            return bytes;
        }
    }
}
