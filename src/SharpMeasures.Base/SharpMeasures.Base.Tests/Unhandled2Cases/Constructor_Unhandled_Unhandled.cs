namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Constructor_Unhandled_Unhandled
{
    private static Unhandled2 Target(Unhandled x, Unhandled y) => new(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchX(Unhandled x, Unhandled y)
    {
        var actual = Target(x, y).X;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchY(Unhandled x, Unhandled y)
    {
        var actual = Target(x, y).Y;

        Assert.Equal(y, actual);
    }
}
