namespace SharpMeasures.Tests.Vector4Cases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Vector4 vector) => vector.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchToStringWithInvariantCulture(Vector4 vector)
    {
        var expected = vector.ToString(CultureInfo.InvariantCulture);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
