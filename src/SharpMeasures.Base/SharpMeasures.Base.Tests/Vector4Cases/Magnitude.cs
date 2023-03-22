namespace SharpMeasures.Tests.Vector4Cases;

using System;

using Xunit;

public class Magnitude
{
    private static Scalar Target(Vector4 vector) => vector.Magnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchArithmetic(Vector4 vector)
    {
        Scalar expected = Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W));

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
