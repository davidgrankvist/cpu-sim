using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Devices
{
    public class ButtonDevice : IDevice
    {
        private readonly int address;
        public bool IsPressed { get; set; }

        public ButtonDevice(int address)
        {
            this.address = address;
        }

        public void Initialize(MappedMemory memory)
        {
            memory.Map(address);
        }

        public void Update(MappedMemory memory)
        {
            if (IsPressed)
            {
                memory.Set(address, 1);
            }
            else
            {
                memory.Set(address, 0);
            }
        }
    }
}
