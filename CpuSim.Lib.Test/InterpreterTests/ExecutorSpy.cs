using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Test.InterpreterTests;

public class ExecutorSpy : IExecutor
{
    private List<ICpuCommand> allCommands = [];
    public List<ICpuCommand> Commands { get; private set; } = [];

    public void Execute(ICpuCommand command)
    {
        Commands.Add(command);
    }

    public void ExecuteAll()
    {
    }

    public void Load(IEnumerable<ICpuCommand> commands)
    {
        allCommands = commands.ToList();
    }
}