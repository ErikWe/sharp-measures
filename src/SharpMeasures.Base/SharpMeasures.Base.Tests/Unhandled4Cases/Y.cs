namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Y
{
    private static Unhandled Target(Unhandled4 vector) => vector.Y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void NoException(Unhandled4 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
