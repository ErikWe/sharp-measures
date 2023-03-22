namespace SharpMeasures.Tests.Vector2Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Vector2 vector) => vector.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchToStringWithInvariantCulture(Vector2 vector)
    {
        var expected = vector.ToString(CultureInfo.InvariantCulture);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
