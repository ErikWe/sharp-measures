namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class Difference_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar other) => scalar.Difference(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchAbsoluteOfToDoubleSubtraction(Scalar scalar, Scalar other)
    {
        var expected = Math.Abs(scalar.ToDouble() - other.ToDouble());

        var actual = Target(scalar, other).ToDouble();

        Assert.Equal(expected, actual);
    }
}
