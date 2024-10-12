namespace CpuSim.Lib.Simulation.Commands.Arithmetic
{
    public struct DivideCommand : ICpuCommand
    {
        private readonly int register1;

        private readonly int register2;

        public DivideCommand(int register1, int register2)
        {
            this.register1 = register1;
            this.register2 = register2;
        }
        public void Execute(CpuState cpuState)
        {
            var v1 = cpuState.GetRegister(register1);
            var v2 = cpuState.GetRegister(register2);
            cpuState.SetRegister(register1, v1 / v2);
        }
    }
}
