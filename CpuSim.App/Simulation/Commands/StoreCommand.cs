namespace CpuSim.App.Simulation.Commands
{
    internal class StoreCommand : ICpuCommand
    {
        private readonly int register;

        private readonly int address;

        public StoreCommand(int register, int address)
        {
            this.register = register;
            this.address = address;
        }
        public void Execute(CpuState cpuState)
        {
            cpuState.Store(register, address);
        }
    }
}
