using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.CpuStates;
using CpuSim.Lib.Simulation.Devices;

namespace CpuSim.Lib.Test.DeviceTests
{
    [TestClass]
    public class DeviceTest
    {
        [TestMethod]
        public void ShouldEnableLedUsingButton()
        {
            var mappedMemory = new MappedMemory();
            var buttonAddress = 0;
            var ledAddress = 1;
            var button = new ButtonDevice(buttonAddress);
            var led = new LedDevice(ledAddress);
            var cpuState = new CpuState(1, mappedMemory);
            var executor = new Executor(cpuState);

            var r = 0;
            var program = new List<ICpuCommand>
            {
                new LoadAddressCommand(r, buttonAddress),
                new StoreCommand(r, ledAddress),
            };

            button.Initialize(mappedMemory);
            led.Initialize(mappedMemory);

            Assert.IsFalse(led.IsOn);

            button.IsPressed = true;
            button.Update(mappedMemory);
            executor.Load(program);
            executor.ExecuteAll();
            led.Update(mappedMemory);

            Assert.IsTrue(led.IsOn);
        }
    }
}
