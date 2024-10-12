namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    public struct CompareCommand : ICpuCommand
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
            var v1 = cpuState.GetRegister(register1);
            var v2 = cpuState.GetRegister(register2);

            CompareResult result;
            if (v1 == v2)
            {
                result = CompareResult.Equal;
            }
            else if (v1 < v2)
            {
                result = CompareResult.LessThan;
            }
            else
            {
                result = CompareResult.GreaterThan;
            }

            cpuState.SetCompareResult(result);
        }
    }
}
