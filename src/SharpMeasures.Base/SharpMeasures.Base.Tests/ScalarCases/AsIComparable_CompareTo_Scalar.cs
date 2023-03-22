namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class AsIComparable_CompareTo_Scalar
{
    private static int Target(Scalar scalar, Scalar other)
    {
        return execute(scalar);

        int execute(IComparable<Scalar> comparable) => comparable.CompareTo(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchScalarCompareTo(Scalar scalar, Scalar other)
    {
        var expected = scalar.CompareTo(other);

        var actual = Target(scalar, other);

        Assert.Equal(expected, actual);
    }
}
