using CpuSim.App.Simulation.Commands;

namespace CpuSim.App.Simulation
{
    internal class Executor
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
