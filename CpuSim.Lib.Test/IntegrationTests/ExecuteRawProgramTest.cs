using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.CpuStates;
using CpuSim.Lib.Simulation.Devices;
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
        private MappedMemory mappedMemory;

        [TestInitialize]
        public void Initialize()
        {
            numRegisters = 10;
            mappedMemory = new MappedMemory();
            cpuState = new CpuState(numRegisters, mappedMemory);
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

        [TestMethod]
        public void ShouldRunCallAndReturnProgram()
        {
            interpreter = new Interpreter(executor, InterpreterExecutionMode.NonInteractivePreload);
            var setup = IntegrationTestDataHelper.CreateCallAndReturnProgram();

            interpreter.Run(setup.Program);

            Assert.AreEqual(1, cpuState.GetMemory(setup.DidFinishAddress));
            Assert.AreEqual(setup.ExpectedResult, cpuState.GetRegister(setup.ResultRegister));
        }

        [TestMethod]
        public void ShouldRunCallAndRecurseCountdownProgram()
        {
            interpreter = new Interpreter(executor, InterpreterExecutionMode.NonInteractivePreload);
            var setup = IntegrationTestDataHelper.CreateCallAndRecurseCountdownProgram();

            interpreter.Run(setup.Program);

            Assert.AreEqual(1, cpuState.GetMemory(setup.DidFinishAddress));
            Assert.AreEqual(setup.ExpectedResult, cpuState.GetRegister(setup.ResultRegister));
        }

        [TestMethod]
        public void ShouldRunFibonacciProgram()
        {
            interpreter = new Interpreter(executor, InterpreterExecutionMode.NonInteractivePreload);
            var setup = IntegrationTestDataHelper.CreateFibonacciProgram();

            interpreter.Run(setup.Program);

            Assert.AreEqual(1, cpuState.GetMemory(setup.DidFinishAddress));
            Assert.AreEqual(setup.ExpectedResult, cpuState.GetRegister(setup.ResultRegister));
        }

        [TestMethod]
        public void ShouldEnableLedUsingButton()
        {
            var setup = IntegrationTestDataHelper.CreateCopyBetweenMemoryProgram();
            var button = new ButtonDevice(setup.FromAddress);
            var led = new LedDevice(setup.ToAddress);
            mappedMemory.Map(setup.ToAddress);
            mappedMemory.Map(setup.FromAddress);

            Assert.IsFalse(led.IsOn);

            button.IsPressed = true;
            button.Update(mappedMemory);
            interpreter.Run(setup.Program);
            led.Update(mappedMemory);

            Assert.IsTrue(led.IsOn);
        }
    }
}
