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
# use extra marks as the call stack is not implemented yet
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
    }
}
