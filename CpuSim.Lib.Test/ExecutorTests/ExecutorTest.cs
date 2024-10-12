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

        private void RunProgram(IEnumerable<ICpuCommand> commands)
        {
            foreach (var command in commands)
            {
                executor.Execute(command);
            }
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

        [TestMethod]
        public void ShouldForwardJump()
        {
            var (program, toSkip, toRun) = ExecutorTestDataHelper.CreateForwardJumpProgram();

            executor.Load(program);
            executor.ExecuteAll();

            Assert.IsFalse(toSkip.DidExecute);
            Assert.IsTrue(toRun.DidExecute);
        }

        [TestMethod]
        public void ShouldLoopUntilLimit()
        {
            var (program, toRepeat, expectedRepetitions) = ExecutorTestDataHelper.CreateIncrementLoopProgram();

            executor.Load(program);
            executor.ExecuteAll();

            Assert.AreEqual(expectedRepetitions, toRepeat.NumExecutions);
        }
    }
}
