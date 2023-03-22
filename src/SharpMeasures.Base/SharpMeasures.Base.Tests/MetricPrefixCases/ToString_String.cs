namespace SharpMeasures.Tests.MetricPrefixCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString_String
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString(string).")]
    private static string Target(MetricPrefix prefix, string? format) => prefix.ToString(format);

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void En_General_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        General_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void En_FloatingPoint_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        FloatingPoint_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void En_Null_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        Null_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void De_General_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        General_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void De_FloatingPoint_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        FloatingPoint_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidMetricPrefix))]
    public void De_Null_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        Null_MatchFactorToStringWithCurrentCulture(prefix);
    }

    private static void General_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        var format = "G";

        MatchFactorToStringWithCurrentCulture(prefix, format);
    }

    private static void FloatingPoint_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        var format = "F4";

        MatchFactorToStringWithCurrentCulture(prefix, format);
    }

    private static void Null_MatchFactorToStringWithCurrentCulture(MetricPrefix prefix)
    {
        string? format = null;

        MatchFactorToStringWithCurrentCulture(prefix, format);
    }

    private static void MatchFactorToStringWithCurrentCulture(MetricPrefix prefix, string? format)
    {
        var expected = prefix.Factor.ToString(format, CultureInfo.CurrentCulture);

        var actual = Target(prefix, format);

        Assert.Equal(expected, actual);
    }
}
