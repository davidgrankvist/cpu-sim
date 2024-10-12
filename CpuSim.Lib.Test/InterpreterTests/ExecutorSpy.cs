using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Test.InterpreterTests;

public class ExecutorSpy : IExecutor
{
    public List<ICpuCommand> Commands { get; } = [];

    public void Execute(ICpuCommand command)
    {
        Commands.Add(command);
    }
}