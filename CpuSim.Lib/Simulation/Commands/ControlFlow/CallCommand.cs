using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct CallCommand : ICpuCommand
    {
        private readonly string mark;

        public CallCommand(string mark)
        {
            this.mark = mark;
        }

        public void Execute(CpuState cpuState)
        {
            var pc = cpuState.GetProgramCounter();
            cpuState.PushReturn(pc + 1);

            var addr = cpuState.GetMarkAddress(mark);
            cpuState.SetProgramCounter(addr);
        }
    }
}
