using CpuSim.App.Experiments;

namespace CpuSim.App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length >= 1 && args[0] == "interactive")
            {
                var experiment = new InteractiveInterpreterExperiment();
                experiment.Run();
            }
            else if (args.Length >= 1 && args[0] == "btnled")
            {
                var experiment = new ButtonAndLedExperiment();
                experiment.Run();
            }
            else
            {
                var experiment = new InteractiveSevenSegmentDisplayExperiment();
                experiment.Run();
            }
        }
    }
}

