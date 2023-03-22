namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Operator_GreaterThan
{
    private static bool Target(BinaryPrefix lhs, BinaryPrefix rhs) => lhs > rhs;

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
    public void NullLHSAndRHS_False()
    {
        var prefix = Datasets.GetNullBinaryPrefix();
        var other = Datasets.GetNullBinaryPrefix();

        var actual = Target(prefix, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_MatchFactorGreaterThan(BinaryPrefix lhs, BinaryPrefix rhs)
    {
        var expected = lhs.Factor > rhs.Factor;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void SameInstance_False(BinaryPrefix prefix)
    {
        var actual = Target(prefix, prefix);

        Assert.False(actual);
    }
}
