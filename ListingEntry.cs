namespace Assembler
{
    public class ListingEntry
    {
        public int Address { get; init; }
        public byte[] Bytes { get; init; } = [];
        public Statement Statement { get; init; } = null!;
        public Token[] Tokens { get; init; } = [];
    }
}