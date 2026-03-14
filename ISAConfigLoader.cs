using System.Text.Json;
using System.Text.Json.Serialization;
namespace Assembler
{
    public static class ISAConfigLoader
    {
        private static JsonSerializerOptions jsonOptions = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
        public static void Load(string path)
        {
            if (!File.Exists(path))
                return;

            var json = File.ReadAllText(path);
            var config = JsonSerializer.Deserialize<ISAConfig>(json, jsonOptions);

            if (config == null)
                return;

            ApplyInstructions(config);
            ApplyRegisters(config);
        }


        private static void ApplyInstructions(ISAConfig config)
        {
            if (config.Instructions == null)
            {
                return;
            }

            InstructionSet.Specs.Clear();
            foreach (var inst in config.Instructions)
            {
                InstructionSet.Specs[inst.Name] = new InstructionSpec(inst.Opcode, inst.Operands);
            }
        }

        private static void ApplyRegisters(ISAConfig config)
        {
            if (config.Registers == null)
            {
                return;
            }

            Register.Codes.Clear();
            foreach (var reg in config.Registers)
            {
                Register.Codes[reg.Name] = reg.Code;
            }
        }
    }
}