namespace SharpMeasures.Tests.MetricPrefixCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(MetricPrefix prefix) => prefix.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void En_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void De_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        MatchFactorToStringWithCurrentCulture(prefix);
    }

    private static void MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        var expected = prefix.Factor.ToString(CultureInfo.CurrentCulture);

        var actual = Target(prefix);

        Assert.Equal(expected, actual);
    }
}
