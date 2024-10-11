using CpuSim.App.Simulation;

namespace CpuSim.App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cpuState = new CpuState();
            var executor = new Executor(cpuState);
            var interpreter = new Interpreter(executor);

            using (var stdin = Console.In)
            {
                interpreter.Run(stdin);
            }
        }
    }
}

