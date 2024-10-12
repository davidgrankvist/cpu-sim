namespace CpuSim.Lib.Simulation.Commands.Arithmetic
{
    internal class DivideCommand : ICpuCommand
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
            cpuState.Add(register1, register2);
        }
    }
}
