﻿using CpuSim.Lib.Simulation.CpuStates;

namespace CpuSim.Lib.Simulation.Commands
{
    public struct LoadCommand : ICpuCommand
    {
        private readonly int register;

        private readonly int value;

        public LoadCommand(int register, int value)
        {
            this.register = register;
            this.value = value;
        }

        public void Execute(CpuState cpuState)
        {
            cpuState.SetRegister(register, value);
        }
    }
}
