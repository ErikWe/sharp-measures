namespace SharpMeasures.Tests.Unhandled4Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(Unhandled4 vector, string? format) => vector.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void General_MatchToStringWithInvariantCulture(Unhandled4 vector)
    {
        var format = "G";

        MatchToStringWithInvariantCulture(vector, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void FloatingPoint_MatchToStringWithInvariantCulture(Unhandled4 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5}, {3:G})";

        MatchToStringWithInvariantCulture(vector, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void Null_MatchToStringWithInvariantCulture(Unhandled4 vector)
    {
        string? format = null;

        MatchToStringWithInvariantCulture(vector, format);
    }

    private static void MatchToStringWithInvariantCulture(Unhandled4 vector, string? format)
    {
        var expected = vector.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(vector, format);

        Assert.Equal(expected, actual);
    }
}
