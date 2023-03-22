namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Negate
{
    private static Vector3 Target(Vector3 vector) => vector.Negate();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchNegatedX(Vector3 vector)
    {
        var expected = vector.X.Negate();

        var actual = Target(vector).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchNegatedY(Vector3 vector)
    {
        var expected = vector.Y.Negate();

        var actual = Target(vector).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchNegatedZ(Vector3 vector)
    {
        var expected = vector.Z.Negate();

        var actual = Target(vector).Z;

        Assert.Equal(expected, actual);
    }
}
