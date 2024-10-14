using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.CpuStates;

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

        [TestMethod]
        public void ShouldCallSubroutineAndReturn()
        {
            numRegisters = 4;
            cpuState = new CpuState(numRegisters);
            executor = new Executor(cpuState);
            var (program, toSkip, resultRegister, expectedResult) = ExecutorTestDataHelper.CreateCallSubroutineProgram();

            executor.Load(program);
            executor.ExecuteAll();

            Assert.IsTrue(!toSkip.DidExecute);
            Assert.AreEqual(expectedResult, cpuState.GetRegister(resultRegister));
        }

        [DataTestMethod]
        [DataRow(false, false)]
        [DataRow(true, false)]
        [DataRow(false, true)]
        [DataRow(true, true)]
        public void ShouldWriteToOneAddressAndCopyToTheNext(bool fromAddressIsMapped, bool toAddressIsMapped)
        {
            var mappedMemory = new MappedMemory();
            cpuState = new CpuState(numRegisters, mappedMemory);
            executor = new Executor(cpuState);
            var fromAddress = 12;
            var toAddress = 13;
            var (program, expectedResult) = ExecutorTestDataHelper.CreateReadWriteMemoryCommand(fromAddress, toAddress);

            if (fromAddressIsMapped)
            {
                mappedMemory.Map(fromAddress);
            }
            if (toAddressIsMapped)
            {
                mappedMemory.Map(toAddress);
            }

            executor.Load(program);
            executor.ExecuteAll();

            Assert.AreEqual(expectedResult, cpuState.GetMemory(fromAddress));
            Assert.AreEqual(expectedResult, cpuState.GetMemory(toAddress));
            if (fromAddressIsMapped)
            {
                Assert.AreEqual(expectedResult, mappedMemory.Get(fromAddress));
            }
            if (toAddressIsMapped)
            {
                Assert.AreEqual(expectedResult, mappedMemory.Get(toAddress));
            }
        }
    }
}
