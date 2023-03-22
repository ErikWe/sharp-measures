namespace SharpMeasures.Tests.Vector3Cases;

using System;

using Xunit;

public class Magnitude
{
    private static Scalar Target(Vector3 vector) => vector.Magnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchArithmetic(Vector3 vector)
    {
        Scalar expected = Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z));

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
