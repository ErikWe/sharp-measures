namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Equals_BinaryPrefix_BinaryPrefix
{
    private static bool Target(BinaryPrefix lhs, BinaryPrefix rhs) => BinaryPrefix.Equals(lhs, rhs);

    [Fact]
    public void NullLHS_False()
    {
        var prefix = Datasets.GetNullBinaryPrefix();
        var other = Datasets.GetValidBinaryPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Fact]
    public void NullRHS_False()
    {
        var prefix = Datasets.GetValidBinaryPrefix();
        var other = Datasets.GetNullBinaryPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Fact]
    public void NullLHSAndRHS_True()
    {
        var prefix = Datasets.GetNullBinaryPrefix();
        var other = Datasets.GetNullBinaryPrefix();

        var actual = Target(prefix, other);

        Assert.True(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_MatchInstanceMethod(BinaryPrefix lhs, BinaryPrefix rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

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
