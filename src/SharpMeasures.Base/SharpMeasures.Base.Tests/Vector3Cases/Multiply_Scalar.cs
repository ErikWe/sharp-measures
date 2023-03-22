namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Multiply_Scalar
{
    private static Vector3 Target(Vector3 vector, Scalar factor) => vector.Multiply(factor);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchXMultiply(Vector3 vector, Scalar factor)
    {
        var expected = vector.X.Multiply(factor);

        var actual = Target(vector, factor).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchYMultiply(Vector3 vector, Scalar factor)
    {
        var expected = vector.Y.Multiply(factor);

        var actual = Target(vector, factor).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidScalar))]
    public void MatchZMultiply(Vector3 vector, Scalar factor)
    {
        var expected = vector.Z.Multiply(factor);

        var actual = Target(vector, factor).Z;

        Assert.Equal(expected, actual);
    }
}
