namespace SharpMeasures.Tests.BinaryPrefixCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(BinaryPrefix prefix) => prefix.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void MatchToStringWithInvariantCulture(BinaryPrefix prefix)
    {
        var expected = prefix.ToString(CultureInfo.InvariantCulture);

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
