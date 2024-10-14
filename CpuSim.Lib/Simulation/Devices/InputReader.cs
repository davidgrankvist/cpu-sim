namespace CpuSim.Lib.Simulation.Devices
{
    public class InputReader
    {
        public InputKey Read()
        {
            InputKey key;
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                key = ToKey(keyInfo.KeyChar);
            }
            else
            {
                key = InputKey.None;
            }

            return key;
        }

        private static InputKey ToKey(char c)
        {
            InputKey key;
            switch (c)
            {
                case '0':
                    key = InputKey.Key0;
                    break;
                case '1':
                    key = InputKey.Key1;
                    break;
                case '2':
                    key = InputKey.Key2;
                    break;
                default:
                    key = InputKey.None;
                    break;
            }

            return key;
        }

        public void Flush()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
