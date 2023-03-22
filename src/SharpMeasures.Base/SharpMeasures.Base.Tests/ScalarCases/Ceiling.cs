namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Ceiling
{
    private static Scalar Target(Scalar scalar) => scalar.Ceiling();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemCeiling(Scalar scalar)
    {
        var expected = Math.Ceiling(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
