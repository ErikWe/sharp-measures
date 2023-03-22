namespace SharpMeasures.Tests.BinaryPrefixCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(BinaryPrefix prefix) => prefix.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void En_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void De_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        MatchFactorToStringWithCurrentCulture(prefix);
    }

    private static void MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        var expected = prefix.Factor.ToString(CultureInfo.CurrentCulture);

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
