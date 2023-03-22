namespace SharpMeasures.Tests.UnhandledCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Unhandled unhandled) => unhandled.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void En_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void De_MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        MatchMagnitudeToStringWithCurrentCulture(unhandled);
    }

    private static void MatchMagnitudeToStringWithCurrentCulture(Unhandled unhandled)
    {
        var expected = unhandled.Magnitude.ToString(CultureInfo.CurrentCulture);

        var actual = Target(unhandled);

        Assert.Equal(expected, actual);
    }
}
