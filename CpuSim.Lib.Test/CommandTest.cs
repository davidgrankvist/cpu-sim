using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.Commands.Arithmetic;
using CpuSim.Lib.Simulation.Commands.ControlFlow;
using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Test
{
    [TestClass]
    public class CommandTest
    {
        private CpuState cpuState;
        private int numRegisters;

        [TestInitialize]
        public void Initialize()
        {
            numRegisters = 2;
            cpuState = new CpuState(numRegisters);
        }

        [TestMethod]
        public void ShouldAddAndStoreInFirst()
        {
            var r1 = 0;
            var r2 = 1;
            var v1 = 1;
            var v2 = 2;
            var cmd = new AddCommand(r1, r2);
            cpuState.SetRegister(r1, v1);
            cpuState.SetRegister(r2, v2);

            cmd.Execute(cpuState);

            Assert.AreEqual(v1 + v2, cpuState.GetRegister(r1));
        }

        [TestMethod]
        public void ShouldSubtractAndStoreInFirst()
        {
            var r1 = 0;
            var r2 = 1;
            var v1 = 1;
            var v2 = 2;
            var cmd = new SubtractCommand(r1, r2);
            cpuState.SetRegister(r1, v1);
            cpuState.SetRegister(r2, v2);

            cmd.Execute(cpuState);

            Assert.AreEqual(v1 - v2, cpuState.GetRegister(r1));
        }

        [TestMethod]
        public void ShouldMultiplyAndStoreInFirst()
        {
            var r1 = 0;
            var r2 = 1;
            var v1 = 1;
            var v2 = 2;
            var cmd = new MultiplyCommand(r1, r2);
            cpuState.SetRegister(r1, v1);
            cpuState.SetRegister(r2, v2);

            cmd.Execute(cpuState);

            Assert.AreEqual(v1 * v2, cpuState.GetRegister(r1));
        }

        [TestMethod]
        public void ShouldDivideAndStoreInFirst()
        {
            var r1 = 0;
            var r2 = 1;
            var v1 = 1;
            var v2 = 2;
            var cmd = new DivideCommand(r1, r2);
            cpuState.SetRegister(r1, v1);
            cpuState.SetRegister(r2, v2);

            cmd.Execute(cpuState);

            Assert.AreEqual(v1 / v2, cpuState.GetRegister(r1));
        }

        [TestMethod]
        public void ShouldIncrement()
        {
            var r = 0;
            var v = 1;
            var cmd = new IncrementCommand(r);
            cpuState.SetRegister(r, v);

            cmd.Execute(cpuState);

            Assert.AreEqual(v + 1, cpuState.GetRegister(r));
        }

        [TestMethod]
        public void ShouldDecrement()
        {
            var r = 0;
            var v = 1;
            var cmd = new DecrementCommand(r);
            cpuState.SetRegister(r, v);

            cmd.Execute(cpuState);

            Assert.AreEqual(v - 1, cpuState.GetRegister(r));
        }


        [TestMethod]
        public void ShouldLoad()
        {
            var r = 0;
            var v = 1;
            var cmd = new LoadCommand(r, v);

            cmd.Execute(cpuState);

            Assert.AreEqual(v, cpuState.GetRegister(r));
        }

        [TestMethod]
        public void ShouldStore()
        {
            var r = 0;
            var v = 1;
            var addr = 2;
            var cmd = new StoreCommand(r, addr);
            cpuState.SetRegister(r, v);

            cmd.Execute(cpuState);

            Assert.AreEqual(v, cpuState.GetMemory(addr));
        }

        [DataTestMethod]
        [DataRow(1, 1, CompareResult.Equal)]
        [DataRow(2, 1, CompareResult.GreaterThan)]
        [DataRow(2, 1, CompareResult.GreaterThanOrEqual)]
        [DataRow(1, 1, CompareResult.GreaterThanOrEqual)]
        [DataRow(1, 2, CompareResult.LessThan)]
        [DataRow(1, 2, CompareResult.LessThanOrEqual)]
        [DataRow(1, 1, CompareResult.LessThanOrEqual)]
        [DataRow(1, 2, CompareResult.NotEqual)]
        [DataRow(2, 1, CompareResult.NotEqual)]
        [DataRow(1, 1, CompareResult.Any)]
        [DataRow(1, 2, CompareResult.Any)]
        [DataRow(2, 1, CompareResult.Any)]
        public void ShouldCompare(int v1, int v2, CompareResult expected)
        {
            var r1 = 0;
            var r2 = 1;
            var cmd = new CompareCommand(r1, r2);
            cpuState.SetRegister(r1, v1);
            cpuState.SetRegister(r2, v2);

            cmd.Execute(cpuState);

            Assert.IsTrue(expected.HasFlag(cpuState.GetCompareResult()));
        }

        [DataTestMethod]
        [DataRow(CompareResult.Equal)]
        [DataRow(CompareResult.GreaterThan)]
        [DataRow(CompareResult.GreaterThanOrEqual)]
        [DataRow(CompareResult.LessThan)]
        [DataRow(CompareResult.LessThanOrEqual)]
        [DataRow(CompareResult.NotEqual)]
        [DataRow(CompareResult.Any)]
        public void ShouldJumpIfMatchingCompareResults(CompareResult targetResult)
        {
            var mark = "marker";
            var markAddr = 2;
            var cmd = new JumpCommand(mark, targetResult);
            cpuState.SetMarkAddress(mark, markAddr);
            cpuState.SetCompareResult(targetResult);

            cmd.Execute(cpuState);

            Assert.AreEqual(markAddr, cpuState.GetProgramCounter());
        }

        [DataTestMethod]
        [DataRow(CompareResult.Equal, CompareResult.NotEqual)]
        [DataRow(CompareResult.GreaterThan, CompareResult.LessThanOrEqual)]
        [DataRow(CompareResult.GreaterThanOrEqual, CompareResult.LessThan)]
        [DataRow(CompareResult.LessThan, CompareResult.GreaterThanOrEqual)]
        [DataRow(CompareResult.LessThanOrEqual, CompareResult.GreaterThan)]
        [DataRow(CompareResult.NotEqual, CompareResult.Equal)]
        public void ShouldNotJumpIfMismatchingCompareResults(CompareResult targetResult, CompareResult actualResult)
        {
            var mark = "marker";
            var markAddr = 2;
            var pc = 1;
            var cmd = new JumpCommand(mark, targetResult);
            cpuState.SetProgramCounter(pc);
            cpuState.SetMarkAddress(mark, markAddr);
            cpuState.SetCompareResult(actualResult);

            cmd.Execute(cpuState);

            Assert.AreEqual(pc + 1, cpuState.GetProgramCounter());
        }

        [TestMethod]
        public void ShouldMarkAtProgramCounter()
        {
            var pc = 1;
            var mark = "marker";
            var cmd = new MarkCommand(mark);
            cpuState.SetProgramCounter(pc);

            cmd.Execute(cpuState);

            Assert.AreEqual(pc, cpuState.GetMarkAddress(mark));
        }

        [TestMethod]
        public void ShouldPushRegisterValue()
        {
            var r = 0;
            var v = 5;
            var cmd = new PushCommand(r);
            cpuState.SetRegister(r, v);

            cmd.Execute(cpuState);
            var popped = cpuState.Pop();

            Assert.AreEqual(v, popped);
        }

        [TestMethod]
        public void ShouldPopIntoRegister()
        {
            var r = 0;
            var v = 5;
            var cmd = new PopCommand(r);
            cpuState.Push(v);

            cmd.Execute(cpuState);

            Assert.AreEqual(v, cpuState.GetRegister(r));
        }

        [TestMethod]
        public void ShouldCallSubroutineAndReturnToCaller()
        {
            var pc = 1;
            var mark = "marker";
            var markAddress = 1337;
            var cmd = new CallCommand(mark);
            cpuState.SetProgramCounter(pc);
            cpuState.SetMarkAddress(mark, markAddress);

            cmd.Execute(cpuState);
            var poppedAddress = cpuState.PopReturn();

            Assert.AreEqual(pc + 1, poppedAddress);
            Assert.AreEqual(markAddress, cpuState.GetProgramCounter());
        }

        [TestMethod]
        public void ShouldReturnToAddress()
        {
            var pcRet = 1;
            var pc = 1337;
            var cmd = new ReturnCommand();
            cpuState.SetProgramCounter(pc);
            cpuState.PushReturn(pcRet);

            cmd.Execute(cpuState);

            Assert.AreEqual(pcRet, cpuState.GetProgramCounter());
        }

        [TestMethod]
        public void ShouldLoadFromRegularAddress()
        {
            var r = 0;
            var addr = 1337;
            var value = 1234;
            var cmd = new LoadAddressCommand(r, addr);
            cpuState.SetMemory(addr, value);

            cmd.Execute(cpuState);

            Assert.AreEqual(value, cpuState.GetRegister(r));
        }

        [TestMethod]
        public void ShouldLoadFromMappedAddress()
        {
            var mappedMemory = new MappedMemory();
            cpuState = new CpuState(numRegisters, mappedMemory);

            var r = 0;
            var addr = 1337;
            var value = 1234;
            var cmd = new LoadAddressCommand(r, addr);
            mappedMemory.Map(addr);
            mappedMemory.Set(addr, value);

            cmd.Execute(cpuState);

            Assert.AreEqual(value, cpuState.GetRegister(r));
        }
    }
}
