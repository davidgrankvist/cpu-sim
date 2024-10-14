namespace CpuSim.Lib.Simulation.Devices
{
    public interface IInputDevice : IDevice
    {
        public void ActiveKey(InputKey key);

        public void DeactiveKey(InputKey key);
    }
}
