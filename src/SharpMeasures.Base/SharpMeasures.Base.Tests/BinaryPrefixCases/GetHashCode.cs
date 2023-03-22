namespace SharpMeasures.Tests.BinaryPrefixCases;

using Xunit;

public class GetHashCode
{
    private static int Target(BinaryPrefix prefix) => prefix.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidExponentInt32))]
    public void Equal_Match(int exponent)
    {
        var prefix = BinaryPrefix.TwoToThePower(exponent);
        var other = BinaryPrefix.TwoToThePower(exponent);

        var expected = Target(other);

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
