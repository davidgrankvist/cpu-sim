using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Simulation
{
    public interface IExecutor
    {
        public void Execute(ICpuCommand command);
    }
}
