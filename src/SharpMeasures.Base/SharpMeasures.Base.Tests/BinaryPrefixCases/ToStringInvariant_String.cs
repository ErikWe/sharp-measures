namespace SharpMeasures.Tests.BinaryPrefixCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(BinaryPrefix prefix, string? format) => prefix.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void General_MatchToStringWithInvariantCulture(BinaryPrefix prefix)
    {
        var format = "G";

        MatchToStringWithInvariantCulture(prefix, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void FloatingPoint_MatchToStringWithInvariantCulture(BinaryPrefix prefix)
    {
        var format = "F4";

        MatchToStringWithInvariantCulture(prefix, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Null_MatchToStringWithInvariantCulture(BinaryPrefix prefix)
    {
        string? format = null;

        MatchToStringWithInvariantCulture(prefix, format);
    }

    private static void MatchToStringWithInvariantCulture(BinaryPrefix prefix, string? format)
    {
        var expected = prefix.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(prefix, format);

        Assert.Equal(expected, actual);
    }
}
