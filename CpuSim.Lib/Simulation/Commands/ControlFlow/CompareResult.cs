namespace CpuSim.Lib.Simulation.Commands.ControlFlow
{
    [Flags]
    public enum CompareResult
    {
        Equal = 1 << 0,
        LessThan = 1 << 1,
        GreaterThan = 1 << 2,
        GreaterThanOrEqual = GreaterThan | Equal,
        LessThanOrEqual = LessThan | Equal,
        NotEqual = LessThan | GreaterThan,
        Any = Equal | LessThan | LessThanOrEqual | GreaterThan | GreaterThanOrEqual | NotEqual,
    }
}
