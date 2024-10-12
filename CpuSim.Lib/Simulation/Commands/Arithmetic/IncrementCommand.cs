namespace CpuSim.Lib.Simulation.Commands.Arithmetic
{
    public struct IncrementCommand : ICpuCommand
    {
        private readonly int register;

        public IncrementCommand(int register)
        {
            this.register = register;
        }

        public void Execute(CpuState cpuState)
        {
            var v = cpuState.GetRegister(register);
            cpuState.SetRegister(register, v - 1);
        }
    }
}
