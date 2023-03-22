namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Power_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar exponent) => scalar.Power(exponent);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchSystemPower(Scalar scalar, Scalar exponent)
    {
        var expected = Math.Pow(scalar, exponent);

        var actual = Target(scalar, exponent).ToDouble();

        Assert.Equal(expected, actual);
    }
}
