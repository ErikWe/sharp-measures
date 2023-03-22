namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Constructor_Vector3
{
    private static Unhandled3 Target(Vector3 components) => new(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchXMagnitude(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target((x, y, z)).X.Magnitude;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchYMagnitude(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target((x, y, z)).Y.Magnitude;

        Assert.Equal(y, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchZMagnitude(Scalar x, Scalar y, Scalar z)
    {
        var actual = Target((x, y, z)).Z.Magnitude;

        Assert.Equal(z, actual);
    }
}
