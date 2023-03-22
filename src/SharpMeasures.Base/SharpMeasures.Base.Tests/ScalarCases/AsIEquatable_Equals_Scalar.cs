namespace SharpMeasures.Tests.ScalarCases;

using System;

using Xunit;

public class AsIEquatable_Equals_Scalar
{
    private static bool Target(Scalar scalar, Scalar other)
    {
        return execute(scalar);

        bool execute(IEquatable<Scalar> equatable) => equatable.Equals(other);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchScalarEquals(Scalar scalar, Scalar other)
    {
        var expected = scalar.Equals(other);

        var actual = Target(scalar, other);

        Assert.Equal(expected, actual);
    }
}
