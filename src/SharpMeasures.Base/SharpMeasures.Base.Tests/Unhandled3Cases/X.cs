namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class X
{
    private static Unhandled Target(Unhandled3 vector) => vector.X;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NoException(Unhandled3 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
