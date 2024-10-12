﻿namespace CpuSim.Lib.Simulation.Commands
{
    public struct StoreCommand : ICpuCommand
    {
        private readonly int register;

        private readonly int address;

        public StoreCommand(int register, int address)
        {
            this.register = register;
            this.address = address;
        }
        public void Execute(CpuState cpuState)
        {
            cpuState.Store(register, address);
        }
    }
}
