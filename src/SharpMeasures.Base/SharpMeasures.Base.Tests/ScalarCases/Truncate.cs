namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Truncate
{
    private static Scalar Target(Scalar scalar) => scalar.Truncate();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemTruncate(Scalar scalar)
    {
        var expected = Math.Truncate(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
