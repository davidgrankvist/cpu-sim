using CpuSim.Lib.Simulation.CpuStates;
using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Devices;

namespace CpuSim.App.Experiments
{
    /// <summary>
    /// Interact with a sevent-segment display.
    /// </summary>
    internal class InteractiveSevenSegmentDisplayExperiment
    {
        private readonly int displayAddress = 0x1337;
        public void Run()
        {
            var mappedMemory = new MappedMemory();
            var cpuState = new CpuState(10, mappedMemory);
            var executor = new Executor(cpuState);
            var interpreter = new Interpreter(executor, InterpreterExecutionMode.Interactive);

            var display = new SevenSegmentDisplayDevice(displayAddress);
            display.Initialize(mappedMemory);

            var prevDisplayValue = 0;
            var displayTask = Task.Run(async () =>
            {
                while (true)
                {
                    display.Update(mappedMemory);
                    var current = mappedMemory.Get(displayAddress);
                    if (current != prevDisplayValue)
                    {
                        Console.Write(StringifyDisplay(display));
                        prevDisplayValue = current;
                    }
                    await Task.Delay(1000);
                }
            });

            Console.WriteLine("Store a value to 0x1337 to update the display. Example:");
            Console.WriteLine("   ld r0 0x77");
            Console.WriteLine("   st r0 0x1337");
            using (var stdin = Console.In)
            {
                interpreter.Run(stdin);
            }
        }

        private static string StringifyDisplay(SevenSegmentDisplayDevice display)
        {
            var aStr = display.a ? "-----" : "     ";
            var gStr = display.g ? "-----" : "     ";
            var dStr = display.d ? "-----" : "     ";
            var fC = display.f ? "|" : " ";
            var bC = display.b ? "|" : " ";
            var eC = display.e ? "|" : " ";
            var cC = display.c ? "|" : " ";

            var str = " " + aStr + " \n";
            str += fC + "     " + bC + "\n";
            str += fC + "     " + bC + "\n";
            str += " " + gStr + " \n";
            str += eC + "     " + cC + "\n";
            str += eC + "     " + cC + "\n";
            str += " " + dStr + " \n";

            return str;
        }
    }
}
