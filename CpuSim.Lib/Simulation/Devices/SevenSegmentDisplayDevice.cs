using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Devices
{
    public class SevenSegmentDisplayDevice : IDevice
    {
        private readonly int address;

        private int segments;

        /*
         *    --a--
         * f |     | b
         *   |     |
         *    --g--
         * e |     | c
         *   |     |
         *    --d--
         */
        public bool a => (segments & 0x40) != 0;
        public bool b => (segments & 0x20) != 0;
        public bool c => (segments & 0x10) != 0;
        public bool d => (segments & 0x08) != 0;
        public bool e => (segments & 0x04) != 0;
        public bool f => (segments & 0x02) != 0;
        public bool g => (segments & 0x01) != 0;

        public SevenSegmentDisplayDevice(int address)
        {
            this.address = address;
        }

        public void Initialize(MappedMemory memory)
        {
            memory.Map(address);
        }

        public void Update(MappedMemory memory)
        {
            var value = memory.Get(address);
            segments = value;
        }
    }
}
