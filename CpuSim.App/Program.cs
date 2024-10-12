using CpuSim.Lib.Simulation;

namespace CpuSim.App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cpuState = new CpuState(10);
            var executor = new Executor(cpuState);
            var interpreter = new Interpreter(executor, true);

            using (var stdin = Console.In)
            {
                interpreter.Run(stdin);
            }
        }
    }
}

