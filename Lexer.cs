namespace Assembler
{
	public class Lexer
	{
		private const char commentChar = ';';
		private const char directiveChar = '.';

        public static List<List<Token>> Tokenize(string[] lines)
		{
			var tokens = new List<List<Token>>();

			lines = FormatLines(lines);

			for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
			{
				var lineTokens = lines[lineNumber].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
				var lineTokenList = new List<Token>();
                if (lineTokens.Length == 0)
					continue;

                for (int tokenIndex = 0; tokenIndex < lineTokens.Length; tokenIndex++)
				{
					var token = lineTokens[tokenIndex];
                    if (InstructionSet.Specs.ContainsKey(token))
					{
                        lineTokenList.Add(new Token(Token.Types.OPCode, token, lineNumber));
					}
					else if (Register.Codes.ContainsKey(token))
					{
                        lineTokenList.Add(new Token(Token.Types.Register, token, lineNumber));
					}
                    else if (token.StartsWith(directiveChar) && Directive.Codes.Contains(token[1..]))
                    {
                        lineTokenList.Add(new Token(Token.Types.Directive, token[1..], lineNumber));
                    }
					else if (int.TryParse(token, out _))
					{
                        lineTokenList.Add(new Token(Token.Types.Number, token, lineNumber));
                    }
                    else
					{
                        lineTokenList.Add(new Token(Token.Types.Label, token, lineNumber));
					}
                }

                tokens.Add(lineTokenList);
            }

			return tokens;
		}

        private static string[] FormatLines(string[] lines)
        {
            var formatted = new List<string>();

            foreach (var rawLine in lines)
            {
                var line = rawLine;
                int commentIndex = line.IndexOf(commentChar);

                if (commentIndex != -1)
                    line = line[..commentIndex];

                line = line.Trim();

                if (!string.IsNullOrWhiteSpace(line))
                    formatted.Add(line);
            }

            return [.. formatted];
        }
    }
}
