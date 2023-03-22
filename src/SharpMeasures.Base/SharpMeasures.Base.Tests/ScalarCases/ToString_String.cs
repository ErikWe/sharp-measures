namespace SharpMeasures.Tests.ScalarCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString_String
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString(string).")]
    private static string Target(Scalar scalar, string? format) => scalar.ToString(format);

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void En_General_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        General_MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void En_FloatingPoint_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        FloatingPoint_MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void En_Null_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        Null_MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void De_General_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        General_MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void De_FloatingPoint_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        FloatingPoint_MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void De_Null_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        Null_MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    private static void General_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        var format = "G";

        MatchToDoubleToStringWithCurrentCulture(scalar, format);
    }

    private static void FloatingPoint_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        var format = "F4";

        MatchToDoubleToStringWithCurrentCulture(scalar, format);
    }

    private static void Null_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        string? format = null;

        MatchToDoubleToStringWithCurrentCulture(scalar, format);
    }

    private static void MatchToDoubleToStringWithCurrentCulture(Scalar scalar, string? format)
    {
        var expected = scalar.ToDouble().ToString(format, CultureInfo.CurrentCulture);

        var actual = Target(scalar, format);

        Assert.Equal(expected, actual);
    }
}
