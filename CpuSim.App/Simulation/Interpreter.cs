using CpuSim.App.Simulation.Commands;
using CpuSim.App.Simulation.Commands.Arithmetic;
using CpuSim.App.Simulation.Commands.ControlFlow;

namespace CpuSim.App.Simulation
{
    internal class Interpreter
    {
        private readonly Executor executor;

        public Interpreter(Executor executor)
        {
            this.executor = executor;
        }

        public void Run(TextReader input)
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                var tokens = line.Trim().Split();
                if (TryParse(tokens, out var command))
                {
                    executor.Execute(command);
                }
            }
        }

        private bool TryParse(string[] tokens, out ICpuCommand command)
        {
            command = null;

            if (tokens.Length >= 3)
            {
                command = ParseTwoArgCommand(tokens[0], tokens[1], tokens[2]);
            }
            else if (tokens.Length == 2)
            {
                command = ParseOneArgCommand(tokens[0], tokens[1]);
            }

            return command != null;
        }

        private ICpuCommand ParseOneArgCommand(string instruction, string arg)
        {
            ICpuCommand command = null;
            switch (instruction)
            {
                case "inc":
                    command = new IncrementCommand(ToRegisterIndex(arg));
                    break;
                case "dec":
                    command = new DecrementCommand(ToRegisterIndex(arg));
                    break;
                case "jmp":
                    command = new JumpCommand(arg, CompareResult.Any);
                    break;
                case "je":
                    command = new JumpCommand(arg, CompareResult.Equal);
                    break;
                case "jne":
                    command = new JumpCommand(arg, CompareResult.NotEqual);
                    break;
                case "jl":
                    command = new JumpCommand(arg, CompareResult.LessThan);
                    break;
                case "jle":
                    command = new JumpCommand(arg, CompareResult.LessThanOrEqual);
                    break;
                case "jg":
                    command = new JumpCommand(arg, CompareResult.GreaterThan);
                    break;
                case "jge":
                    command = new JumpCommand(arg, CompareResult.GreaterThanOrEqual);
                    break;
            }

            return command;
        }


        private ICpuCommand ParseTwoArgCommand(string instruction, string arg1, string arg2)
        {
            ICpuCommand command = null;
            switch (instruction)
            {
                case "add":
                    command = new AddCommand(ToRegisterIndex(arg1), ToRegisterIndex(arg2));
                    break;
                case "sub":
                    command = new SubtractCommand(ToRegisterIndex(arg1), ToRegisterIndex(arg2));
                    break;
                case "mul":
                    command = new MultiplyCommand(ToRegisterIndex(arg1), ToRegisterIndex(arg2));
                    break;
                case "div":
                    command = new DivideCommand(ToRegisterIndex(arg1), ToRegisterIndex(arg2));
                    break;
                case "cmp":
                    command = new CompareCommand(ToRegisterIndex(arg1), ToRegisterIndex(arg2));
                    break;
                case "ld":
                    command = new LoadCommand(ToRegisterIndex(arg1), int.Parse(arg2));
                    break;
                case "st":
                    command = new StoreCommand(ToRegisterIndex(arg1), int.Parse(arg2));
                    break;
            }

            return command;
        }

        private static int ToRegisterIndex(string registerString)
        {
            if (registerString.First() != 'r')
            {
                throw new InvalidOperationException("Registers must consist of an r followed by a number (e.g. r0)");
            }
            return int.Parse(registerString.Substring(1));
        }
    }
}
