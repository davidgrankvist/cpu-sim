using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.Commands.Arithmetic;
using CpuSim.Lib.Simulation.Commands.ControlFlow;

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

        public static (IEnumerable<ICpuCommand> Program, CommandSpy ToSkip, CommandSpy ToRun) CreateForwardJumpProgram()
        {
            var toSkip  = new CommandSpy();
            var toRun = new CommandSpy();

            var r = 0;
            var program = new List<ICpuCommand>()
            {
                new JumpCommand("end", CompareResult.Any),
                toSkip,
                new MarkCommand("end"),
                toRun,
            };

            return (program, toSkip, toRun);
        }

        public static (IEnumerable<ICpuCommand> Program, CommandSpy ToRepeat, int ExpectedRepetitions) CreateIncrementLoopProgram()
        {
            var expectedRepetitions = 10;
            var toRepeat = new CommandSpy();

            var r1 = 0;
            var r2 = 1;
            var program = new List<ICpuCommand>()
            {
                new LoadCommand(r1, 0),
                new LoadCommand(r2, expectedRepetitions),
                new MarkCommand("loop"),
                toRepeat,
                new IncrementCommand(r1),
                new CompareCommand(r1, r2),
                new JumpCommand("loop", CompareResult.LessThan),
            };

            return (program, toRepeat, expectedRepetitions);
        }
    }
}
