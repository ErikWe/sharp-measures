namespace SharpMeasures.Tests.Unhandled3Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(Unhandled3 vector, string? format) => vector.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void General_MatchToStringWithInvariantCulture(Unhandled3 vector)
    {
        var format = "G";

        MatchToStringWithInvariantCulture(vector, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void FloatingPoint_MatchToStringWithInvariantCulture(Unhandled3 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5})";

        MatchToStringWithInvariantCulture(vector, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void Null_MatchToStringWithInvariantCulture(Unhandled3 vector)
    {
        string? format = null;

        MatchToStringWithInvariantCulture(vector, format);
    }

    private static void MatchToStringWithInvariantCulture(Unhandled3 vector, string? format)
    {
        var expected = vector.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(vector, format);

        Assert.Equal(expected, actual);
    }
}
