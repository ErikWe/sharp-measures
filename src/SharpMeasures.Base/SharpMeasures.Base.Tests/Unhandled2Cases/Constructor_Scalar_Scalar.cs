namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Constructor_Scalar_Scalar
{
    private static Unhandled2 Target(Scalar x, Scalar y) => new(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchXMagnitude(Scalar x, Scalar y)
    {
        var actual = Target(x, y).X.Magnitude;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandledTuple))]
    public void MatchYMagnitude(Scalar x, Scalar y)
    {
        var actual = Target(x, y).Y.Magnitude;

        Assert.Equal(y, actual);
    }
}
