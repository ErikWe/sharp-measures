namespace SharpMeasures.Tests.Unhandled4Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString_String
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString(string).")]
    private static string Target(Unhandled4 vector, string? format) => vector.ToString(format);

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void En_General_MatchWithCurrentCulture(Unhandled4 vector)
    {
        General_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void En_FloatingPoint_MatchWithCurrentCulture(Unhandled4 vector)
    {
        FloatingPoint_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void En_Null_MatchWithCurrentCulture(Unhandled4 vector)
    {
        Null_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void De_General_MatchWithCurrentCulture(Unhandled4 vector)
    {
        General_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void De_FloatingPoint_MatchWithCurrentCulture(Unhandled4 vector)
    {
        FloatingPoint_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void De_Null_MatchWithCurrentCulture(Unhandled4 vector)
    {
        Null_MatchWithCurrentCulture(vector);
    }

    private static void General_MatchWithCurrentCulture(Unhandled4 vector)
    {
        var format = "G";

        MatchWithCurrentCulture(vector, format);
    }

    private static void FloatingPoint_MatchWithCurrentCulture(Unhandled4 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5}, {3:G})";

        MatchWithCurrentCulture(vector, format);
    }

    private static void Null_MatchWithCurrentCulture(Unhandled4 vector)
    {
        string? format = null;

        MatchWithCurrentCulture(vector, format);
    }

    private static void MatchWithCurrentCulture(Unhandled4 vector, string? format)
    {
        if (format is "g" or "G" or null)
        {
            format = "({0}, {1}, {2}, {3})";
        }

        var expected = string.Format(CultureInfo.CurrentCulture, format, vector.X, vector.Y, vector.Z, vector.W);

        var actual = Target(vector, format);

        Assert.Equal(expected, actual);
    }
}
