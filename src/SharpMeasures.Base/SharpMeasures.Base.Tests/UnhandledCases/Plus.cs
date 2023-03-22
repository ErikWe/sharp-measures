namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Plus
{
    private static Unhandled Target(Unhandled unhandled) => unhandled.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void EqualOriginal(Unhandled unhandled)
    {
        var actual = Target(unhandled);

        Assert.Equal(unhandled, actual);
    }
}
