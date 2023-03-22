namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Equals_Object
{
    private static bool Target(BinaryPrefix prefix, object? other) => prefix.Equals(other);

    [Fact]
    public void Null_False()
    {
        var prefix = Datasets.GetValidBinaryPrefix();
        object? other = null;

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var prefix = Datasets.GetValidBinaryPrefix();
        var other = string.Empty;

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void SameType_MatchBinaryPrefixEquals(BinaryPrefix prefix, BinaryPrefix other)
    {
        var expected = prefix.Equals(other);

        var actual = Target(prefix, other);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void SameInstance_True(BinaryPrefix prefix)
    {
        var actual = Target(prefix, prefix);

        Assert.True(actual);
    }
}
