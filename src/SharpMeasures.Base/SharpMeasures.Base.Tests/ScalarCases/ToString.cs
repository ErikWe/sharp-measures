namespace SharpMeasures.Tests.ScalarCases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Scalar scalar) => scalar.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void En_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void De_MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        MatchToDoubleToStringWithCurrentCulture(scalar);
    }

    private static void MatchToDoubleToStringWithCurrentCulture(Scalar scalar)
    {
        var expected = scalar.ToDouble().ToString(CultureInfo.CurrentCulture);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
