namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class CompareTo_Scalar
{
    private static int Target(Scalar scalar, Scalar other) => scalar.CompareTo(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchSignOfToDoubleCompareTo(Scalar scalar, Scalar other)
    {
        var expected = Math.Sign(scalar.ToDouble().CompareTo(other.ToDouble()));

        var actual = Math.Sign(Target(scalar, other));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_OneOfAllowedValues(Scalar scalar, Scalar other)
    {
        var allowed = new[] { 1, 0, -1 };

        var actual = Target(scalar, other);

        Assert.Contains(actual, allowed);
    }
}
