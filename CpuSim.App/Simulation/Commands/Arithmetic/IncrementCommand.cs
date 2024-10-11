namespace CpuSim.App.Simulation.Commands.Arithmetic
{
    internal class IncrementCommand : ICpuCommand
    {
        private readonly int register;

        public IncrementCommand(int register)
        {
            this.register = register;
        }

        public void Execute(CpuState cpuState)
        {
            cpuState.Increment(register);
        }
    }
}
