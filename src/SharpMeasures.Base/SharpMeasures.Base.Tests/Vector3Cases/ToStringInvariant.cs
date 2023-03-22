namespace SharpMeasures.Tests.Vector3Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Vector3 vector) => vector.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchToStringWithInvariantCulture(Vector3 vector)
    {
        var expected = vector.ToString(CultureInfo.InvariantCulture);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
