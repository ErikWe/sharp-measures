namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Constructor_Vector2
{
    private static Unhandled2 Target(Vector2 components) => new(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchXMagnitude(Scalar x, Scalar y)
    {
        var actual = Target((x, y)).X.Magnitude;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchYMagnitude(Scalar x, Scalar y)
    {
        var actual = Target((x, y)).Y.Magnitude;

        Assert.Equal(y, actual);
    }
}
