namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    internal class CompareCommand : ICpuCommand
    {
        private readonly int register1;


        private readonly int register2;

        public CompareCommand(int register1, int register2)
        {
            this.register1 = register1;
            this.register2 = register2;
        }

        public void Execute(CpuState cpuState)
        {
            cpuState.Compare(register1, register2);
        }
    }
}
