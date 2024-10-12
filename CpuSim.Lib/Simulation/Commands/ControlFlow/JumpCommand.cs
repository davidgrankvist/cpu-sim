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
            cpuState.Jump(marker, compareResult);
        }
    }
}
