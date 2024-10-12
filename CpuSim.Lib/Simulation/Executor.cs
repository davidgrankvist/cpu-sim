using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.Commands.ControlFlow;

namespace CpuSim.Lib.Simulation
{
    public class Executor : IExecutor
    {
        private readonly CpuState cpuState;
        private readonly List<ICpuCommand> commands;

        public Executor(CpuState cpuState)
        {
            this.cpuState = cpuState;
            commands = [];
        }

        /// <summary>
        /// Execute a command and any additional commands if a backwards jump is made.
        /// </summary>
        public void Execute(ICpuCommand command)
        {
            commands.Add(command);
            ExecuteAll();
        }

        /// <summary>
        /// Preload so that marks can be registered.
        /// </summary>
        public void Load(IEnumerable<ICpuCommand> commands)
        {
            var commandsList = commands.ToList();
            for (var i  = 0; i < commandsList.Count; i++)
            {
                var command = commandsList[i];
                this.commands.Add(command);

                if (command is MarkCommand)
                {
                    cpuState.SetProgramCounter(i);
                    command.Execute(cpuState);
                    cpuState.SetProgramCounter(0);
                }
            }
        }

        /// <summary>
        /// Execute loaded commands until the program counter reaches the end of the program.
        /// </summary>
        public void ExecuteAll()
        {
            var hasNext = true;
            while ((hasNext = ExecuteNext()))
            {
            }
        }

        private bool ExecuteNext()
        {
            var pc = cpuState.GetProgramCounter();

            if (pc >= commands.Count)
            {
                return false;
            }

            var command = commands[pc];
            command.Execute(cpuState);

            if (command is not JumpCommand jumpCommand || !jumpCommand.ShouldJump(cpuState))
            {
                cpuState.SetProgramCounter(pc + 1);
            }

            return true;
        }
    }
}
