namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;

using Xunit;

public class AsIComparable_CompareTo_BinaryPrefix
{
    private static int Target(BinaryPrefix prefix, BinaryPrefix other)
    {
        return execute(prefix);

        int execute(IComparable<BinaryPrefix> comparable) => comparable.CompareTo(other);
    }

    [Fact]
    public void Null_One()
    {
        var prefix = Datasets.GetValidBinaryPrefix();
        var other = Datasets.GetNullBinaryPrefix();

        var actual = Target(prefix, other);

        Assert.Equal(1, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_MatchSignOfFactorompareTo(BinaryPrefix prefix, BinaryPrefix other)
    {
        var expected = Math.Sign(prefix.Factor.CompareTo(other.Factor));

        var actual = Math.Sign(Target(prefix, other));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_OneOfAllowedValues(BinaryPrefix prefix, BinaryPrefix other)
    {
        var allowed = new[] { 1, 0, -1 };

        var actual = Target(prefix, other);

        Assert.Contains(actual, allowed);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void SameInstance_Zero(BinaryPrefix prefix)
    {
        var actual = Target(prefix, prefix);

        Assert.Equal(0, actual);
    }
}
