using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Devices
{
    public interface IDevice
    {
        void Initialize(MappedMemory memory);

        void Update(MappedMemory memory);
    }
}
