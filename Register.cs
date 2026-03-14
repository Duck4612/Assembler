using System;
namespace Assembler
{
    public static class Register
    {
        public static readonly Dictionary<string, byte> Codes = new(StringComparer.OrdinalIgnoreCase)
        {
            ["R0"] = 0,
            ["R1"] = 1,
            ["R2"] = 2,
            ["R3"] = 3,
            ["R4"] = 4,
            ["R5"] = 5,
            ["SP"] = 6,
            ["FP"] = 7,
            ["SL"] = 8,
            ["Z"] = 9,
            ["SB"] = 10,
            ["PC"] = 11
        };
    }
}