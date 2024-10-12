using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.Commands.Arithmetic;

namespace CpuSim.Lib.Test.ExecutorTests
{
    internal static class ExecutorTestDataHelper
    {
        public static (IEnumerable<ICpuCommand> Program, int ResultRegister, int ExpectedResult) CreateCombinedArithmeticProgram()
        {
            var expectedResult = 5 + 3 * 6 / 2 - 2;
            var resultRegister = 0;

            var r1 = resultRegister;
            var r2 = 1;
            var program = new List<ICpuCommand>()
            {
                new LoadCommand(r1, 3),
                new LoadCommand(r2, 6),
                new MultiplyCommand(r1, r2),
                new LoadCommand(r2, 2),
                new DivideCommand(r1, r2),
                new LoadCommand(r2, 5),
                new AddCommand(r1, r2),
                new LoadCommand(r2, 2),
                new SubtractCommand(r1, r2),
            };

            return (program, r1, expectedResult);
        }

        public static (IEnumerable<ICpuCommand> Program, int ResultAddress, int ExpectedResult) CreateLoadIncStoreProgram()
        {
            var expectedResult = 5;
            var resultAddress = 0x1337;

            var r = 0;
            var program = new List<ICpuCommand>()
            {
                new LoadCommand(r, 4),
                new IncrementCommand(r),
                new StoreCommand(r, resultAddress),
            };

            return (program, resultAddress, expectedResult);
        }
    }
}
