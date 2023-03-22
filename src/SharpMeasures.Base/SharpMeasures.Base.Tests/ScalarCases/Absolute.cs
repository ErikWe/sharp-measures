namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Absolute
{
    private static Scalar Target(Scalar scalar) => scalar.Absolute();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemAbs(Scalar scalar)
    {
        var expected = Math.Abs(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
