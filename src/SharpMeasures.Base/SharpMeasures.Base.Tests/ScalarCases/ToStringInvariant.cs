namespace SharpMeasures.Tests.ScalarCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant
{
    private static string Target(Scalar scalar) => scalar.ToStringInvariant();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchToStringWithInvariantCulture(Scalar scalar)
    {
        var expected = scalar.ToString(CultureInfo.InvariantCulture);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
