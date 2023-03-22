namespace SharpMeasures.Tests.Unhandled4Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Unhandled4 vector) => vector.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void En_MatchCustomWithCurrentCulture(Unhandled4 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void De_MatchCustomWithCurrentCulture(Unhandled4 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    private static void MatchCustomWithCurrentCulture(Unhandled4 vector)
    {
        var expected = string.Format(CultureInfo.CurrentCulture, "({0}, {1}, {2}, {3})", vector.X, vector.Y, vector.Z, vector.W);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
