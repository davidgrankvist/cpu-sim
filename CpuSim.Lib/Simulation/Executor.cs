using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Simulation
{
    public class Executor
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
