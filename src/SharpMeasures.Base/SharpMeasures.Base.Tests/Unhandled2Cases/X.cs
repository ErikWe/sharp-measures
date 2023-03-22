namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class X
{
    private static Unhandled Target(Unhandled2 vector) => vector.X;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void NoException(Unhandled2 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
