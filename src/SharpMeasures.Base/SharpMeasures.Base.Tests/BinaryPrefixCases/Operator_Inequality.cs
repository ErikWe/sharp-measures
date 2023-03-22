namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(BinaryPrefix lhs, BinaryPrefix rhs) => lhs != rhs;

    [Fact]
    public void NullLHS_True()
    {
        var lhs = Datasets.GetNullBinaryPrefix();
        var rhs = Datasets.GetValidBinaryPrefix();

        var actual = Target(lhs, rhs);

        Assert.True(actual);
    }

    [Fact]
    public void NullRHS_True()
    {
        var lhs = Datasets.GetValidBinaryPrefix();
        var rhs = Datasets.GetNullBinaryPrefix();

        var actual = Target(lhs, rhs);

        Assert.True(actual);
    }

    [Fact]
    public void NullLHSAndRHS_False()
    {
        var lhs = Datasets.GetNullBinaryPrefix();
        var rhs = Datasets.GetNullBinaryPrefix();

        var actual = Target(lhs, rhs);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix_ValidBinaryPrefix))]
    public void Valid_OppositeOfEqualsMethod(BinaryPrefix lhs, BinaryPrefix rhs)
    {
        var expected = lhs.Equals(rhs) is false;

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
