namespace SharpMeasures.Tests.Vector2Cases;

using System;

using Xunit;

public class Magnitude
{
    private static Scalar Target(Vector2 vector) => vector.Magnitude();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchArithmetic(Vector2 vector)
    {
        Scalar expected = Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
