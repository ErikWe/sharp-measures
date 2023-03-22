namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Constructor
{
    private static Vector2 Target(Scalar x, Scalar y) => new(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchX(Scalar x, Scalar y)
    {
        var actual = Target(x, y).X;

        Assert.Equal(x, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchY(Scalar x, Scalar y)
    {
        var actual = Target(x, y).Y;

        Assert.Equal(y, actual);
    }
}
