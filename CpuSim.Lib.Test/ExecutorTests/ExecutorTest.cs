using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Test.ExecutorTests
{
    [TestClass]
    public class ExecutorTest
    {
        private Executor executor;
        private CpuState cpuState;
        private int numRegisters;

        [TestInitialize]
        public void Initialize()
        {
            numRegisters = 2;
            cpuState = new CpuState(numRegisters);
            executor = new Executor(cpuState);
        }

        [TestMethod]
        public void ShouldCombineArithmetic()
        {
            var (program, resultRegister, expectedResult) = ExecutorTestDataHelper.CreateCombinedArithmeticProgram();

            RunProgram(program);

            Assert.AreEqual(expectedResult, cpuState.GetRegister(resultRegister));
        }

        [TestMethod]
        public void ShouldLoadIncStore()
        {
            var (program, resultAddress, expectedResult) = ExecutorTestDataHelper.CreateLoadIncStoreProgram();

            RunProgram(program);

            Assert.AreEqual(expectedResult, cpuState.GetMemory(resultAddress));
        }

        private void RunProgram(IEnumerable<ICpuCommand> commands)
        {
            foreach (var command in commands)
            {
                executor.Execute(command);
            }
        }
    }
}
