using CpuSim.Lib.Simulation;
using CpuSim.Lib.Test.Framework;

namespace CpuSim.Lib.Test.IntegrationTests
{
    [TestClass]
    public class ExecuteRawProgramTest
    {
        private Interpreter interpreter;
        private Executor executor;
        private CpuState cpuState;
        private int numRegisters;

        [TestInitialize]
        public void Initialize()
        {
            numRegisters = 10;
            cpuState = new CpuState(numRegisters);
            executor = new Executor(cpuState);
            interpreter = new Interpreter(executor);
        }

        [TestMethod]
        public void ShouldRunMultipleCallAndJumpProgram()
        {
            interpreter = new Interpreter(executor, InterpreterExecutionMode.NonInteractivePreload);
            var setup = IntegrationTestDataHelper.CreateMultipleCallAndJumpProgram();

            interpreter.Run(setup.Program);

            Assert.AreEqual(1, cpuState.GetMemory(setup.DidFinishAddress));
            Assert.AreEqual(setup.FirstResult, cpuState.GetMemory(setup.FirstResultAddress));
            Assert.AreEqual(setup.SecondResult, cpuState.GetMemory(setup.SecondResultAddress));
        }
    }
}
