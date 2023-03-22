namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Magnitude
{
    private static Scalar Target(Unhandled unhandled) => unhandled.Magnitude;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void NoException(Unhandled unhandled)
    {
        var exception = Record.Exception(() => Target(unhandled));

        Assert.Null(exception);
    }
}
