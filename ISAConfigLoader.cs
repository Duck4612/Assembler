using System.Text.Json;
namespace Assembler
{
    public static class ISAConfigLoader
    {
        public static void Load(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var config = JsonSerializer.Deserialize<ISAConfig>(json);

                ApplyInstructions(config);
                ApplyRegisters(config);
            }

            else
            {
                InstructionSet.SetSpecsToDefault();
                Register.SetCodesToDefault();
            }
        }


        private static void ApplyInstructions(ISAConfig? config)
        {
            if (config?.Instructions == null)
            {
                InstructionSet.SetSpecsToDefault();
                return;
            }

            foreach (var inst in config.Instructions)
            {
                InstructionSet.Specs[inst.Name] =
                    new InstructionSpec(inst.Opcode, inst.OperandFormat);
            }
        }

        private static void ApplyRegisters(ISAConfig? config)
        {
            if (config?.Registers == null)
            {
                Register.SetCodesToDefault();
                return;
            }

            foreach (var reg in config.Registers)
            {
                Register.Codes[reg.Name] = reg.Code;
            }
        }
    }
}