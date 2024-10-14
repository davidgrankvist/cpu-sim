using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Commands
{
    public interface ICpuCommand
    {
        void Execute(CpuState cpuState);
    }
}
