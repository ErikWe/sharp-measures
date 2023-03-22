namespace SharpMeasures.Tests.Vector4Cases;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Xunit;

public class ToString
{
    [SuppressMessage("Globalization", "CA1305: Specify IFormatProvider", Justification = "Test-case for ToString().")]
    private static string Target(Vector4 vector) => vector.ToString();

    [Theory]
    [UseCulture("en")]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void En_MatchCustomWithCurrentCulture(Vector4 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    [Theory]
    [UseCulture("de")]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void De_MatchCustomWithCurrentCulture(Vector4 vector)
    {
        MatchCustomWithCurrentCulture(vector);
    }

    private static void MatchCustomWithCurrentCulture(Vector4 vector)
    {
        var expected = string.Format(CultureInfo.CurrentCulture, "({0}, {1}, {2}, {3})", vector.X, vector.Y, vector.Z, vector.W);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
