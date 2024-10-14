using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct PushCommand : ICpuCommand
    {
        private readonly int register;

        public PushCommand(int register)
        {
            this.register = register;
        }

        public void Execute(CpuState cpuState)
        {
            var value = cpuState.GetRegister(register);
            cpuState.Push(value);
        }
    }
}
