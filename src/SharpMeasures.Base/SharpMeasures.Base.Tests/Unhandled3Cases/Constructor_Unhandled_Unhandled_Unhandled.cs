namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Constructor_Unhandled_Unhandled_Unhandled
{
    private static Unhandled3 Target(Unhandled x, Unhandled y, Unhandled z) => new(x, y, z);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchX(Unhandled x, Unhandled y, Unhandled z)
    {
        var actual = Target(x, y, z).X;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchY(Unhandled x, Unhandled y, Unhandled z)
    {
        var actual = Target(x, y, z).Y;

        Assert.Equal(y, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchZ(Unhandled x, Unhandled y, Unhandled z)
    {
        var actual = Target(x, y, z).Z;

        Assert.Equal(z, actual);
    }
}
