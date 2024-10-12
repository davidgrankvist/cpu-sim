namespace CpuSim.Lib.Simulation.Commands
{
    public struct StoreCommand : ICpuCommand
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
            var v = cpuState.GetRegister(register);
            cpuState.SetMemory(address, v);
        }
    }
}
