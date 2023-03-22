namespace SharpMeasures.Tests.BinaryPrefixCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString_String
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString(string).")]
    private static string Target(BinaryPrefix prefix, string? format) => prefix.ToString(format);

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void En_General_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        General_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void En_FloatingPoint_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        FloatingPoint_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void En_Null_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        Null_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void De_General_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        General_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void De_FloatingPoint_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        FloatingPoint_MatchFactorToStringWithCurrentCulture(prefix);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void De_Null_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        Null_MatchFactorToStringWithCurrentCulture(prefix);
    }

    private static void General_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        var format = "G";

        MatchFactorToStringWithCurrentCulture(prefix, format);
    }

    private static void FloatingPoint_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        var format = "F4";

        MatchFactorToStringWithCurrentCulture(prefix, format);
    }

    private static void Null_MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix)
    {
        string? format = null;

        MatchFactorToStringWithCurrentCulture(prefix, format);
    }

    private static void MatchFactorToStringWithCurrentCulture(BinaryPrefix prefix, string? format)
    {
        var expected = prefix.Factor.ToString(format, CultureInfo.CurrentCulture);

        var actual = Target(prefix, format);

        Assert.Equal(expected, actual);
    }
}
