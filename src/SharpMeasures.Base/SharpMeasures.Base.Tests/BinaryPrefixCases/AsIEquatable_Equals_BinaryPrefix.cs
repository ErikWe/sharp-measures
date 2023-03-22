namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class AsIEquatable_Equals_BinaryPrefix
{
    private static bool Target(BinaryPrefix prefix, BinaryPrefix other)
    {
        return execute(prefix);

        bool execute(IEquatable<BinaryPrefix> equatable) => equatable.Equals(other);
    }

    [Fact]
    public void Null_False()
    {
        var prefix = Datasets.GetValidBinaryPrefix();
        var other = Datasets.GetNullBinaryPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_MatchFactorEquals(BinaryPrefix prefix, BinaryPrefix other)
    {
        var expected = prefix.Factor.Equals(other.Factor);

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
