namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Subtract_Vector3
{
    private static Vector3 Target(Vector3 vector, Vector3 subtrahend) => vector.Subtract(subtrahend);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchXSubtract(Vector3 vector, Vector3 subtrahend)
    {
        var expected = vector.X.Subtract(subtrahend.X);

        var actual = Target(vector, subtrahend).X;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchYSubtract(Vector3 vector, Vector3 subtrahend)
    {
        var expected = vector.Y.Subtract(subtrahend.Y);

        var actual = Target(vector, subtrahend).Y;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchZSubtract(Vector3 vector, Vector3 subtrahend)
    {
        var expected = vector.Z.Subtract(subtrahend.Z);

        var actual = Target(vector, subtrahend).Z;

        Assert.Equal(expected, actual);
    }
}
