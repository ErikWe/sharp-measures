namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Round
{
    private static Scalar Target(Scalar scalar) => scalar.Round();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchSystemRound(Scalar scalar)
    {
        var expected = Math.Round(scalar);

        var actual = Target(scalar).ToDouble();

        Assert.Equal(expected, actual);
    }
}
