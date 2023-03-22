namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Y
{
    private static Unhandled Target(Unhandled3 vector) => vector.Y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NoException(Unhandled3 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
