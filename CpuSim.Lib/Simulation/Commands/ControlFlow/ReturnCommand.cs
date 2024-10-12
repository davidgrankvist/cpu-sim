namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct ReturnCommand : ICpuCommand
    {
        public void Execute(CpuState cpuState)
        {
            var address = cpuState.PopReturn();
            cpuState.SetProgramCounter(address);
        }
    }
}
