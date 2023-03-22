namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Remainder_Scalar
{
    private static Vector3 Target(Vector3 vector, Scalar divisor) => vector.Remainder(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchXRemainder(Vector3 vector, Scalar divisor)
    {
        var expected = vector.X.Remainder(divisor);

        var actual = Target(vector, divisor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchYRemainder(Vector3 vector, Scalar divisor)
    {
        var expected = vector.Y.Remainder(divisor);

        var actual = Target(vector, divisor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchZRemainder(Vector3 vector, Scalar divisor)
    {
        var expected = vector.Z.Remainder(divisor);

        var actual = Target(vector, divisor).Z;

        Assert.Equal(expected, actual);
    }
}
