using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Commands
{
    public struct LoadAddressCommand : ICpuCommand
    {
        private readonly int register;
        private readonly int address;

        public LoadAddressCommand(int register, int address)
        {
            this.register = register;
            this.address = address;
        }

        public void Execute(CpuState cpuState)
        {
            var value = cpuState.GetMemory(address);
            cpuState.SetRegister(register, value);
        }
    }
}
