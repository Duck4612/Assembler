namespace Assembler
{
	public class AsmLoader
	{
		public static string[] ReadLines(string path)
		{
            try
            {
                string[] lines = File.ReadAllLines(path);
                return lines;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to read file {path}: {ex.Message}");
            }
        }
    }
}

