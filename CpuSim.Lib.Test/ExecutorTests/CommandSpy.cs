using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.CpuStates;

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
