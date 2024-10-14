using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct MarkCommand : ICpuCommand
    {
        private readonly string mark;

        public MarkCommand(string mark)
        {
            this.mark = mark;
        }

        public void Execute(CpuState cpuState)
        {
            var pc = cpuState.GetProgramCounter();
            cpuState.SetMarkAddress(mark, pc);
        }
    }
}
