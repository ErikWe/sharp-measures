namespace SharpMeasures.Tests.UnhandledCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString_String
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString(string).")]
    private static string Target(Unhandled unhandled, string? format) => unhandled.ToString(format);

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void En_General_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        General_MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void En_FloatingPoint_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        FloatingPoint_MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void En_Null_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        Null_MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void De_General_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        General_MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void De_FloatingPoint_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        FloatingPoint_MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void De_Null_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        Null_MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    private static void General_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        var format = "G";

        MatchMagnitudeToStringWithCurrentCulture(unhandled, format);
    }

    private static void FloatingPoint_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        var format = "F4";

        MatchMagnitudeToStringWithCurrentCulture(unhandled, format);
    }

    private static void Null_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        string? format = null;

        MatchMagnitudeToStringWithCurrentCulture(unhandled, format);
    }

    private static void MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled, string? format)
    {
        var expected = unhandled.Magnitude.ToString(format, CultureInfo.CurrentCulture);

        var actual = Target(unhandled, format);

        Assert.Equal(expected, actual);
    }
}
