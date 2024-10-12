namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct JumpCommand : ICpuCommand
    {
        private readonly string marker;
        private readonly CompareResult compareResult;

        public JumpCommand(string marker, CompareResult compareResult)
        {
            this.marker = marker;
            this.compareResult = compareResult;
        }

        public void Execute(CpuState cpuState)
        {
            if (ShouldJump(cpuState))
            {
                var addr = cpuState.GetMarkAddress(marker);
                cpuState.SetProgramCounter(addr);
            }
        }

        public bool ShouldJump(CpuState cpuState)
        {
            var cmp = cpuState.GetCompareResult();
            return compareResult.HasFlag(cmp);
        }
    }
}
