using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Simulation
{
    public class Executor : IExecutor
    {
        private readonly CpuState cpuState;

        public Executor(CpuState cpuState)
        {
            this.cpuState = cpuState;
        }

        public void Execute(ICpuCommand command)
        {
            command.Execute(cpuState);
        }
    }
}
