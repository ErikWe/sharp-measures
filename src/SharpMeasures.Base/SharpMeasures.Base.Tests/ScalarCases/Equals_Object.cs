namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Scalar scalar, object? other) => scalar.Equals(other);

    [Fact]
    public void Null_False()
    {
        var scalar = Datasets.GetValidScalar();
        object? other = null;

        var actual = Target(scalar, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var scalar = Datasets.GetValidScalar();
        var other = string.Empty;

        var actual = Target(scalar, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void SameType_MatchScalarEquals(Scalar scalar, Scalar other)
    {
        var expected = scalar.Equals(other);

        var actual = Target(scalar, other);

        Assert.Equal(expected, actual);
    }
}
