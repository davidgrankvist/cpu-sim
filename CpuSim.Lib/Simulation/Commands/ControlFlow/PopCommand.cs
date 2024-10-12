namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct PopCommand : ICpuCommand
    {
        private readonly int register;

        public PopCommand(int register)
        {
            this.register = register;
        }

        public void Execute(CpuState cpuState)
        {
            var value = cpuState.Pop();
            cpuState.SetRegister(register, value);
        }
    }
}
