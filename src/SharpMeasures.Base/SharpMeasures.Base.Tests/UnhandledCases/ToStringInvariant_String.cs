namespace SharpMeasures.Tests.UnhandledCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(Unhandled unhandled, string? format) => unhandled.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void General_MatchToStringWithInvariantCulture(Unhandled unhandled)
    {
        var format = "G";

        MatchToStringWithInvariantCulture(unhandled, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void FloatingPoint_MatchToStringWithInvariantCulture(Unhandled unhandled)
    {
        var format = "F4";

        MatchToStringWithInvariantCulture(unhandled, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled))]
    public void Null_MatchToStringWithInvariantCulture(Unhandled unhandled)
    {
        string? format = null;

        MatchToStringWithInvariantCulture(unhandled, format);
    }

    private static void MatchToStringWithInvariantCulture(Unhandled unhandled, string? format)
    {
        var expected = unhandled.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(unhandled, format);

        Assert.Equal(expected, actual);
    }
}
