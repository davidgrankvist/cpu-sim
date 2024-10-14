using CpuSim.Lib.Simulation.CpuStates;
using CpuSim.Lib.Simulation;

namespace CpuSim.App.Experiments
{
    /// <summary>
    /// Read one assembly instruction at a time from STDIN.
    /// </summary>
    internal class InteractiveInterpreterExperiment
    {
        public void Run()
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
