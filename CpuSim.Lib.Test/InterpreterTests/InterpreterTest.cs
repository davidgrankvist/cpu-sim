using CpuSim.Lib.Simulation;
using CpuSim.Lib.Simulation.Commands;

namespace CpuSim.Lib.Test.InterpreterTests;

[TestClass]
public class InterpreterTest
{
    private Interpreter interpreter;
    private ExecutorSpy executorSpy;

    [TestInitialize]
    public void Initialize()
    {
        executorSpy = new();
        interpreter = new(executorSpy);
    }

    private void TestProgram(string program, IEnumerable<ICpuCommand> commands)
    {
        RunProgram(program);
        AssertExtensions.SequenceEqual(commands, executorSpy.Commands);
    }

    private void RunProgram(string program)
    {
        using (var input = new StringReader(program))
        {
            interpreter.Run(input);
        }
    }

    private void TestCrashingProgram(string program, IEnumerable<ICpuCommand> commands)
    {
        AssertExtensions.Throws(() => RunProgram(program));
        AssertExtensions.SequenceEqual(commands, executorSpy.Commands);
    }

    [TestMethod]
    public void ShouldParseValidCommands()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateReferenceProgram();
        TestProgram(program, commands);
    }

    [TestMethod]
    public void ShouldIgnoreWhitespace()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateWhitespaceProgram();
        TestProgram(program, commands);
    }

    [TestMethod]
    public void ShouldCrashFullyInvalidProgram()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateFullyInvalidProgram();
        TestCrashingProgram(program, commands);
    }

    [TestMethod]
    public void ShouldCrashAtInvalidInstruction()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateFullyInvalidInstructionProgram();
        TestCrashingProgram(program, commands);
    }

    [TestMethod]
    public void ShouldCrashAtInvalidRegisterArgument()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateInvalidRegisterInstructionProgram();
        TestCrashingProgram(program, commands);
    }

    [TestMethod]
    public void ShouldCrashAtInvalidValueArgument()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateInvalidValueInstructionProgram();
        TestCrashingProgram(program, commands);
    }

    [TestMethod]
    public void ShouldCrashAtTooManyArguments()
    {
        var (program, commands) = InterpreterTestDataHelper.CreateInvalidValueInstructionProgram();
        TestCrashingProgram(program, commands);
    }

    [TestMethod]
    public void ShouldCrashBeforeExecutionInPreloadMode()
    {
        interpreter = new Interpreter(executorSpy, InterpreterExecutionMode.NonInteractivePreload);
        var (program, commands) = InterpreterTestDataHelper.CreateFullyInvalidInstructionProgram();
        TestCrashingProgram(program, []);
    }
}