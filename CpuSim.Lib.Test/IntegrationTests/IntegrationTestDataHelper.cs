 namespace CpuSim.Lib.Test.IntegrationTests
{
    internal static class IntegrationTestDataHelper
    {
        public static (string Program, int FirstResult, int FirstResultAddress, int SecondResult, int SecondResultAddress, int DidFinishAddress) CreateMultipleCallAndJumpProgram()
        {
            /*
             * Steps:
             * - Calculate some math twice
             * - Store in separate memory addresses
             * - Also store sanity check that we reached the end
             */
            var program = @"
main:
    # first call, x = 7
    ld r0 7
    ld r1 0
    jmp doMath
# use extra marks instead of call stack
# jump to right before we store the result
continuePoint1:
    st r0 1337
    # second call, x = 14
    ld r0 14
    ld r1 1
    jmp doMath
continuePoint2:
    st r0 1338
    jmp end

# Does this: 5 * 2 + x / 7 - 39
# Input: 
#    - x is read from register r0
#    - return address is read from r1
# Output:
#    - result is stored in r0
doMath:
    # x / 7
    ld r2 7
    div r0 r2
    # + 5 * 2
    ld r2 5
    ld r3 2
    mul r2 r3
    add r0 r2
    # - 39
    ld r2 39
    sub r0 r2
    # if r1 is 0, go to continuePoint1, else go to continuePoint2
    ld r2 0
    cmp r1 r2
    je continuePoint1
    jmp continuePoint2

end:
    # indicate that we reached the end
    ld r0 1
    st r0 1339
    # Done! The result should be:
    #   - value at 1337 = 5 * 2 + 7 / 7 - 39  = -28
    #   - value at 1338 = 5 * 2 + 14 / 7 - 39  = -27
    #   - value at 1339 = 1
";
            var firstResult = DoMath(7);
            var firstResultAddress = 1337;
            var secondResult = DoMath(14);
            var secondResultAddress = 1338;
            var didFinishAddress = 1339;
            return (program, firstResult, firstResultAddress, secondResult, secondResultAddress, didFinishAddress);
        }

        private static int DoMath(int x)
        {
            return 5 * 2 + x / 7 - 39;
        }

        public static (string Program, int ResultRegister, int ExpectedResult, int DidFinishAddress) CreateCallAndReturnProgram()
        {
            var program = @"
main:
    ld r0 10
    psh r0
    ld r0 2
    psh r0
    call addSubroutine
    pop r0
    jmp end

addSubroutine:
    pop r0
    pop r1
    add r0 r1
    psh r0
    ret

end:
    ld r1 1
    st r1 1337
";
            var resultRegister = 0;
            var expectedResult = 12;
            var didFinishAddress = 1337;

            return (program, resultRegister, expectedResult, didFinishAddress);
        }

        public static (string Program, int ResultRegister, int ExpectedResult, int DidFinishAddress) CreateCallAndRecurseCountdownProgram()
        {
            var program = @"
main:
    ld r0 10
    psh r0
    call countdown
    pop r0
    jmp end

countdown:
    pop r0
    ld r1 0
    cmp r0 r1
    je countdownDone
    dec r0
    psh r0
    call countdown
countdownDone:
    psh r0
    ret

end:
    ld r1 1
    st r1 1337
";
            var resultRegister = 0;
            var expectedResult = 0;
            var didFinishAddress = 1337;

            return (program, resultRegister, expectedResult, didFinishAddress);
        }
        public static (string Program, int ResultRegister, int ExpectedResult, int DidFinishAddress) CreateFibonacciProgram()
        {
            var program = @"
main:
    ld r0 6
    psh r0
    call fib
    pop r0
    jmp end

fib:
    ld r1 1
    pop r0
    cmp r0 r1
    jg fibRecurse
fibDone:
    psh r0
    ret
fibRecurse:
    # ---- fib(n - 2) ----
    # load n into r1
    psh r0
    pop r1

    # n - 2
    dec r1
    dec r1

    # backup n
    psh r0

    # invoke fib(n - 2), load result into r1
    psh r1
    call fib
    pop r1

    # ---- fib(n - 1) ----
    # restore n
    pop r0

    # n - 1
    dec r0

    # backup fib(n - 2) result
    psh r1

    # invoke fib(n - 1), load result into r0
    psh r0
    call fib
    pop r0

    # restore fib(n - 2) result
    pop r1

    # ---- Result ----
    # add and return result
    add r0 r1
    psh r0
    ret

end:
    ld r1 1
    st r1 1337
";
            var resultRegister = 0;
            var expectedResult = Fib(6);
            var didFinishAddress = 1337;

            return (program,  resultRegister, expectedResult, didFinishAddress);
        }

        private static int Fib(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            return Fib(n - 2) + Fib(n - 1);
        }
    }
}
