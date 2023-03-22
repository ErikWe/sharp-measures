namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(BinaryPrefix lhs, BinaryPrefix rhs) => lhs == rhs;

    [Fact]
    public void NullLHS_False()
    {
        var lhs = Datasets.GetNullBinaryPrefix();
        var rhs = Datasets.GetValidBinaryPrefix();

        var actual = Target(lhs, rhs);

        Assert.False(actual);
    }

    [Fact]
    public void NullRHS_False()
    {
        var lhs = Datasets.GetValidBinaryPrefix();
        var rhs = Datasets.GetNullBinaryPrefix();

        var actual = Target(lhs, rhs);

        Assert.False(actual);
    }

    [Fact]
    public void NullLHSAndRHS_True()
    {
        var lhs = Datasets.GetNullBinaryPrefix();
        var rhs = Datasets.GetNullBinaryPrefix();

        var actual = Target(lhs, rhs);

        Assert.True(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_MatchEqualsMethod(BinaryPrefix lhs, BinaryPrefix rhs)
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
