namespace SharpMeasures.Tests.MetricPrefixCases;

using Xunit;

public class GetHashCode
{
    private static int Target(MetricPrefix prefix) => prefix.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidExponentInt32))]
    public void Equal_Match(int exponent)
    {
        var prefix = MetricPrefix.TenToThePower(exponent);
        var other = MetricPrefix.TenToThePower(exponent);

        var expected = Target(other);

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
