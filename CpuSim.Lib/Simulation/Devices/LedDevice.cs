using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Devices
{
    public class LedDevice : IDevice
    {
        private readonly int address;

        public bool IsOn { get; private set; }

        public LedDevice(int address)
        {
            this.address = address;
        }

        public void Initialize(MappedMemory memory)
        {
            memory.Map(address);
        }

        public void Update(MappedMemory memory)
        {
            IsOn = memory.Get(address) == 1;
        }
    }
}
