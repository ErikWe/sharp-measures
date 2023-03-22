namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Constructor_Scalar_Scalar_Scalar
{
    private static Unhandled3 Target(Scalar x, Scalar y, Scalar z) => new(x, y, z);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchXMagnitude(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target(x, y, z).X.Magnitude;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchYMagnitude(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target(x, y, z).Y.Magnitude;

        Assert.Equal(y, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchZMagnitude(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target(x, y, z).Z.Magnitude;

        Assert.Equal(z, actual);
    }
}
