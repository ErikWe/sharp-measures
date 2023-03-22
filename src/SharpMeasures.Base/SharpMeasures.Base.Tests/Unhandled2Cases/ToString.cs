namespace SharpMeasures.Tests.Unhandled2Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Unhandled2 vector) => vector.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void En_MatchCustomWithCurrentCulture(Unhandled2 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void De_MatchCustomWithCurrentCulture(Unhandled2 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    private static void MatchCustomWithCurrentCulture(Unhandled2 vector)
    {
        var expected = string.Format(CultureInfo.CurrentCulture, "({0}, {1})", vector.X, vector.Y);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
