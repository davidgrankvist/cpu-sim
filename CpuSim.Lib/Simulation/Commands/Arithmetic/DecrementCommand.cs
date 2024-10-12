namespace CpuSim.Lib.Simulation.Commands.Arithmetic
{
    public struct DecrementCommand : ICpuCommand
    {
        private readonly int register;

        public DecrementCommand(int register)
        {
            this.register = register;
        }

        public void Execute(CpuState cpuState)
        {
            cpuState.Decrement(register);
        }
    }
}
