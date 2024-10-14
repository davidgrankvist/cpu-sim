using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cpuState = new CpuState(10);
            var executor = new Executor(cpuState);
            var interpreter = new Interpreter(executor, InterpreterExecutionMode.Interactive);

            using (var stdin = Console.In)
            {
                interpreter.Run(stdin);
            }
        }
    }
}

