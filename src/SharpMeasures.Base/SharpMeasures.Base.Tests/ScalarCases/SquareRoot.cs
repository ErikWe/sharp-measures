namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class SquareRoot
{
    private static Scalar Target(Scalar scalar) => scalar.SquareRoot();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemSqrt(Scalar scalar)
    {
        var expected = Math.Sqrt(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
