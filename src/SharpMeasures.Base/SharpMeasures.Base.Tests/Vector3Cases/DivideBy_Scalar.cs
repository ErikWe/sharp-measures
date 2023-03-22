namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class DivideBy_Scalar
{
    private static Vector3 Target(Vector3 vector, Scalar divisor) => vector.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchXDivideBy(Vector3 vector, Scalar divisor)
    {
        var expected = vector.X.DivideBy(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchYDivideBy(Vector3 vector, Scalar divisor)
    {
        var expected = vector.Y.DivideBy(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchZDivideBy(Vector3 vector, Scalar divisor)
    {
        var expected = vector.Z.DivideBy(divisor);

        var actual = Target(vector, divisor).Z;

        Assert.Equal(expected, actual);
    }
}
