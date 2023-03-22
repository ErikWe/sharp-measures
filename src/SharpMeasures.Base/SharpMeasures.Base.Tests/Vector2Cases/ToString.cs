namespace SharpMeasures.Tests.Vector2Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Vector2 vector) => vector.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void En_MatchCustomWithCurrentCulture(Vector2 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void De_MatchCustomWithCurrentCulture(Vector2 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    private static void MatchCustomWithCurrentCulture(Vector2 vector)
    {
        var expected = string.Format(CultureInfo.CurrentCulture, "({0}, {1})", vector.X, vector.Y);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
