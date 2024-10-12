namespace CpuSim.Lib.Test;

public static class AssertExtensions
{
    public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
    {
        Assert.IsTrue(expected.SequenceEqual(actual));
    }

    public static void Throws(Action action)
    {
        var didThrow = false;
        try
        {
            action();
        }
        catch
        {
            didThrow = true;
        }
        Assert.IsTrue(didThrow);
    }
}