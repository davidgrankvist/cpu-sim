using CpuSim.Lib.Simulation;

namespace CpuSim.Lib.Test.Framework
{
    internal static class InterpreterTestExtensions
    {
        public static void Run(this Interpreter interpreter, string program)
        {
            using (var input = new StringReader(program))
            {
                interpreter.Run(input);
            }
        }
    }
}
