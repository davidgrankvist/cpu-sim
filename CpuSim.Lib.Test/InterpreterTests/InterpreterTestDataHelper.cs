using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.Commands.Arithmetic;
using CpuSim.Lib.Simulation.Commands.ControlFlow;

namespace CpuSim.Lib.Test.InterpreterTests
{
    internal static class InterpreterTestDataHelper
    {
        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateReferenceProgram()
        {
            var program = @"
ld r1 1337
st r1 0x1337
add r1 r2
sub r1 r2
mul r1 r2
div r1 r2
inc r1
dec r1
cmp r1 r2
jmp mark
je mark
jne mark
jl mark
jle mark
jg mark
jge mark
mark:
mixedCaseMarkWithSomeNumbers12151:
# this is a comment
";

            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
                new StoreCommand(1, 0x1337),
                new AddCommand(1, 2),
                new SubtractCommand(1, 2),
                new MultiplyCommand(1, 2),
                new DivideCommand(1, 2),
                new IncrementCommand(1),
                new DecrementCommand(1),
                new CompareCommand(1, 2),
                new JumpCommand("mark", CompareResult.Any),
                new JumpCommand("mark", CompareResult.Equal),
                new JumpCommand("mark", CompareResult.NotEqual),
                new JumpCommand("mark", CompareResult.LessThan),
                new JumpCommand("mark", CompareResult.LessThanOrEqual),
                new JumpCommand("mark", CompareResult.GreaterThan),
                new JumpCommand("mark", CompareResult.GreaterThanOrEqual),
                new MarkCommand("mark"),
                new MarkCommand("mixedCaseMarkWithSomeNumbers12151"),
            };

            return (program, commands);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateWhitespaceProgram()
        {
            var program = @"


            ld      r1   1337   


";
            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
            };

            return (program, commands);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateFullyInvalidProgram()
        {
            var program = @"
INVALID
";

            return (program, []);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateFullyInvalidInstructionProgram()
        {
            var program = @"
ld r1 1337
INVALID
ld r1 1337
";
            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
            };
            return (program, commands);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateInvalidRegisterInstructionProgram()
        {
            var program = @"
ld r1 1337
ld INVALID 1337
";
            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
            };
            return (program, commands);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateInvalidValueInstructionProgram()
        {
            var program = @"
ld r1 1337
ld r1 INVALID
";
            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
            };
            return (program, commands);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateTooManyArgumentsProgram()
        {
            var program = @"
ld r1 1337
ld r1 1337 1337
";
            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
            };
            return (program, commands);
        }

        public static (string Program, IEnumerable<ICpuCommand> Commands) CreateProgramWithCommentedOutCode()
        {
            var program = @"
# here is a comment before the code
ld r1 1337
# below is some commented out code
# ld r1 1337
";
            var commands = new List<ICpuCommand>()
            {
                new LoadCommand(1, 1337),
            };
            return (program, commands);
        }
    }
}
