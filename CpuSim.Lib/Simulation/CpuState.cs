using CpuSim.Lib.Simulation.Commands.ControlFlow;

namespace CpuSim.Lib.Simulation
{
    public class CpuState
    {
        private readonly int[] registers;

        private readonly Dictionary<int, int> memory;

        private CompareResult compareResult;

        private int programCounter;

        private readonly Dictionary<string, int> markToAddress;

        public CpuState(int numRegisters)
        {
            registers = new int[numRegisters];
            memory = [];
            markToAddress = [];
            compareResult = CompareResult.Any;
        }

        public void SetRegister(int register, int value)
        {
            registers[register] = value;
        }

        public int GetRegister(int register)
        {
            return registers[register];
        }

        public void SetMemory(int address, int value)
        {
            memory[address] = value;
        }

        public int GetMemory(int address)
        {
            return memory[address];
        }

        public void SetCompareResult(CompareResult compareResult)
        {
            this.compareResult = compareResult;
        }

        public CompareResult GetCompareResult()
        {
            return compareResult;
        }

        public void SetProgramCounter(int value)
        {
            programCounter = value;
        }

        public int GetProgramCounter()
        {
            return programCounter;
        }

        public void SetMarkAddress(string mark, int address)
        {
            markToAddress[mark] = address;
        }

        public int GetMarkAddress(string mark)
        {
            return markToAddress[mark];
        }
    }
}
