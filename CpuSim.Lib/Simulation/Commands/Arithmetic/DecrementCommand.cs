using CpuSim.Lib.Simulation.CpuStates;

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
            var v = cpuState.GetRegister(register);
            cpuState.SetRegister(register, v - 1);
        }
    }
}
