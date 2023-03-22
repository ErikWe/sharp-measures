namespace SharpMeasures.Tests.Vector3Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString_String
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString(string).")]
    private static string Target(Vector3 vector, string? format) => vector.ToString(format);

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void En_General_MatchWithCurrentCulture(Vector3 vector)
    {
        General_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void En_FloatingPoint_MatchWithCurrentCulture(Vector3 vector)
    {
        FloatingPoint_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void En_Null_MatchWithCurrentCulture(Vector3 vector)
    {
        Null_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void De_General_MatchWithCurrentCulture(Vector3 vector)
    {
        General_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void De_FloatingPoint_MatchWithCurrentCulture(Vector3 vector)
    {
        FloatingPoint_MatchWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void De_Null_MatchWithCurrentCulture(Vector3 vector)
    {
        Null_MatchWithCurrentCulture(vector);
    }

    private static void General_MatchWithCurrentCulture(Vector3 vector)
    {
        var format = "G";

        MatchWithCurrentCulture(vector, format);
    }

    private static void FloatingPoint_MatchWithCurrentCulture(Vector3 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5})";

        MatchWithCurrentCulture(vector, format);
    }

    private static void Null_MatchWithCurrentCulture(Vector3 vector)
    {
        string? format = null;

        MatchWithCurrentCulture(vector, format);
    }

    private static void MatchWithCurrentCulture(Vector3 vector, string? format)
    {
        if (format is "g" or "G" or null)
        {
            format = "({0}, {1}, {2})";
        }

        var expected = string.Format(CultureInfo.CurrentCulture, format, vector.X, vector.Y, vector.Z);

        var actual = Target(vector, format);

        Assert.Equal(expected, actual);
    }
}
