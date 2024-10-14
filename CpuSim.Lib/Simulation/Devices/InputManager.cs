namespace CpuSim.Lib.Simulation.Devices
{
    public class InputManager
    {
        private readonly Dictionary<InputKey, IInputDevice> inputDevices;

        public InputManager()
        {
            inputDevices = [];
        }

        public void MapKey(InputKey key, IInputDevice device)
        {
            inputDevices[key] = device;
        }

        public void ActivateKey(InputKey key)
        {
            if (inputDevices.ContainsKey(key))
            {
                inputDevices[key].ActiveKey(key);
            }
        }

        public void DeactivateKey(InputKey key)
        {
            if (inputDevices.ContainsKey(key))
            {
                inputDevices[key].DeactiveKey(key);
            }
        }
    }
}
