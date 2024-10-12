using CpuSim.Lib.Simulation.Commands;
using CpuSim.Lib.Simulation.Commands.Arithmetic;
using CpuSim.Lib.Simulation.Commands.ControlFlow;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CpuSim.Lib.Simulation
{
    public class Interpreter
    {
        private readonly IExecutor executor;
        private readonly InterpreterExecutionMode executionMode;

        private static readonly Regex markRegex = new Regex("[a-zA-Z0-9]+:");


        public Interpreter(IExecutor executor, InterpreterExecutionMode executionMode)
        {
            this.executor = executor;
            this.executionMode = executionMode;
        }

        public Interpreter(IExecutor executor) : this(executor, InterpreterExecutionMode.NonInteractiveImmediate)
        {
        }

        public void Run(TextReader input)
        {
            switch (executionMode)
            {
                case InterpreterExecutionMode.Interactive:
                    RunInteractive(input);
                    break;
                case InterpreterExecutionMode.NonInteractiveImmediate:
                    RunNonInteractiveImmediate(input);
                    break;
                case InterpreterExecutionMode.NonInteractivePreload:
                    RunNonInteractiveWithPreload(input);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported execution mode");
            }
        }

        public void RunInteractive(TextReader input)
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                try
                {
                    ParseAndExecute(line);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }

        public void RunNonInteractiveWithPreload(TextReader input)
        {
            string line;
            List<ICpuCommand> commands = [];
            while ((line = input.ReadLine()) != null)
            {
                if (TryParseLine(line, out var command))
                {
                    commands.Add(command);
                };
            }

            executor.Load(commands);
            executor.ExecuteAll();
        }

        public void RunNonInteractiveImmediate(TextReader input)
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                ParseAndExecute(line);
            }
        }

        private void ParseAndExecute(string line)
        {
            if (TryParseLine(line, out var command))
            {
                executor.Execute(command);
            }
        }

        private bool TryParseLine(string line, out ICpuCommand command)
        {
            command = null;
            var tokens = Tokenize(line);
            if (tokens.Length > 0 && TryParse(tokens, out var cmd))
            {
                command = cmd;
            }

            return command != null;
        }

        private static string[] Tokenize(string line)
        {
            return line
                .Trim()
                .Split()
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
        }

        private bool TryParse(string[] tokens, out ICpuCommand command)
        {
            command = null;

            if (tokens.Length == 3)
            {
                command = ParseTwoArgCommand(tokens[0], tokens[1], tokens[2]);
            }
            else if (tokens.Length == 2)
            {
                command = ParseOneArgCommand(tokens[0], tokens[1]);
            }
            else if (tokens.Length == 1)
            {
                command = ParseZeroArgCommand(tokens[0]);
            }
            else
            {
                throw new InvalidOperationException($"Unknown command {string.Join(' ')}");
            }

            return command != null;
        }

        private ICpuCommand ParseZeroArgCommand(string instruction)
        {
            ICpuCommand command = null;
            if (markRegex.IsMatch(instruction))
            {
                command = new MarkCommand(instruction.Substring(0, instruction.Length - 1));
            }
            else
            {
                throw new InvalidOperationException($"Invalid zero-argument command: {instruction}");
            }

            return command;
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
                    command = new LoadCommand(ToRegisterIndex(arg1), ParseInt(arg2));
                    break;
                case "st":
                    command = new StoreCommand(ToRegisterIndex(arg1), ParseInt(arg2));
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

        private static int ParseInt(string str)
        {
            int result;
            if (str.StartsWith("0x"))
            {
                result = int.Parse(str.Substring(2), NumberStyles.HexNumber);
            }
            else
            {
                result = int.Parse(str, NumberStyles.Integer);
            }
            return result;
        }
    }
}
