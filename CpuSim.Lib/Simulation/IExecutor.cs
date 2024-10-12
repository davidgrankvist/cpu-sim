using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Simulation
{
    public interface IExecutor
    {
        /// <summary>
        /// Execute a command and any additional commands if a backwards jump is made.
        /// </summary>
        public void Execute(ICpuCommand command);

        /// <summary>
        /// Preload so that marks can be registered.
        /// </summary>
        public void Load(IEnumerable<ICpuCommand> commands);

        /// <summary>
        /// Execute loaded commands until the program counter reaches the end of the program.
        /// </summary>
        public void ExecuteAll();
    }
}
