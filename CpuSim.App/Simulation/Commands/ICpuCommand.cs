namespace CpuSim.App.Simulation.Commands
{
    internal interface ICpuCommand
    {
        void Execute(CpuState cpuState);
    }
}
