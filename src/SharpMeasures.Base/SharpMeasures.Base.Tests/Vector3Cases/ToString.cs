namespace SharpMeasures.Tests.Vector3Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Vector3 vector) => vector.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void En_MatchCustomWithCurrentCulture(Vector3 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void De_MatchCustomWithCurrentCulture(Vector3 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    private static void MatchCustomWithCurrentCulture(Vector3 vector)
    {
        var expected = string.Format(CultureInfo.CurrentCulture, "({0}, {1}, {2})", vector.X, vector.Y, vector.Z);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
