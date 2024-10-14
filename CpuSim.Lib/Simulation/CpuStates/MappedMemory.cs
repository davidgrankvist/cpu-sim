namespace CpuSim.Lib.Simulation.CpuStates
{
    /// <summary>
    /// Simulates memory that's shared between I/O controllers and the CPU.
    /// </summary>
    public class MappedMemory
    {
        private readonly Dictionary<int, int> memory = [];

        public void Map(int address)
        {
            if (memory.ContainsKey(address))
            {
                throw new InvalidOperationException($"Address {address} is already mapped");
            }
            memory[address] = 0;
        }

        public int Get(int address)
        {
            return memory[address];
        }

        public void Set(int address, int value)
        {
            memory[address] = value;
        }

        public bool IsMapped(int address)
        {
            return memory.ContainsKey(address);
        }
    }
}
