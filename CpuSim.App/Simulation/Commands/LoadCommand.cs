namespace CpuSim.App.Simulation.Commands
{
    internal class LoadCommand : ICpuCommand
    {
        private readonly int register;

        private readonly int value;

        public LoadCommand(int register, int value)
        {
            this.register = register;
            this.value = value;
        }

        public void Execute(CpuState cpuState)
        {
            cpuState.Load(register, value);
        }
    }
}
