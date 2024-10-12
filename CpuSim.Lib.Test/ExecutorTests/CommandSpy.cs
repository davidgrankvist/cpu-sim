using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Test.ExecutorTests
{
    internal class CommandSpy : ICpuCommand
    {
        public int NumExecutions { get; private set;  }

        public bool DidExecute => NumExecutions > 0;

        public void Execute(CpuState cpuState)
        {
            NumExecutions++;
        }
    }
}
