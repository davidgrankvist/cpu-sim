using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.CpuStates;
using CpuSim.Lib.Simulation.Devices;

namespace CpuSim.App.Experiments
{
    /// <summary>
    /// Simulate clicking a button to turn a LED on or off.
    /// </summary>
    internal class ButtonAndLedExperiment
    {
        private readonly string program = @"
loop:
    lda r0 0x1337
    st r0 0x1338
    jmp loop
";
        private readonly int buttonAddress = 0x1337;
        private readonly int ledAddres = 0x1338;

        public void Run()
        {
            var mappedMemory = new MappedMemory();
            var cpuState = new CpuState(10, mappedMemory);
            var executor = new Executor(cpuState);
            var interpreter = new Interpreter(executor);

            var button = new ButtonDevice(buttonAddress);
            var led = new LedDevice(ledAddres);
            mappedMemory.Map(buttonAddress);
            mappedMemory.Map(ledAddres);

            var inputManager = new InputManager();
            inputManager.MapKey(InputKey.Key1, button);

            var inputReader = new InputReader();

            var programTask = Task.Run(() =>
            {
                using (var input = new StringReader(program))
                {
                    interpreter.Run(input);
                }
            });

            Console.WriteLine("Press 1 to click the button");
            var prevKey = InputKey.None;
            var prevLedOn = false;
            while (true)
            {
                var key = inputReader.Read();
                inputReader.Flush();
                if (key != prevKey)
                {
                    inputManager.DeactivateKey(prevKey);
                    inputManager.ActivateKey(key);
                    prevKey = key;

                    button.Update(mappedMemory);
                }

                // give the program time to copy 
                Thread.Sleep(50);

                led.Update(mappedMemory);
                if (led.IsOn != prevLedOn)
                {
                    Console.WriteLine("LED " + (led.IsOn ? "ON" : "OFF"));
                    prevLedOn = led.IsOn;
                }
            }
        }
    }
}
