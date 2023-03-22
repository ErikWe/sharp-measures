namespace SharpMeasures.Tests.Vector2Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(Vector2 vector, string? format) => vector.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void General_MatchToStringWithInvariantCulture(Vector2 vector)
    {
        var format = "G";

        MatchToStringWithInvariantCulture(vector, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void FloatingPoint_MatchToStringWithInvariantCulture(Vector2 vector)
    {
        var format = "({0:F4}, {1:F3})";

        MatchToStringWithInvariantCulture(vector, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void Null_MatchToStringWithInvariantCulture(Vector2 vector)
    {
        string? format = null;

        MatchToStringWithInvariantCulture(vector, format);
    }

    private static void MatchToStringWithInvariantCulture(Vector2 vector, string? format)
    {
        var expected = vector.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(vector, format);

        Assert.Equal(expected, actual);
    }
}
