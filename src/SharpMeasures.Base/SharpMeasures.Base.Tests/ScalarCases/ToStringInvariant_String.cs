namespace SharpMeasures.Tests.ScalarCases;

using System.Globalization;

using Xunit;

public class ToStringInvariant_String
{
    private static string Target(Scalar scalar, string? format) => scalar.ToStringInvariant(format);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void General_MatchToStringWithInvariantCulture(Scalar scalar)
    {
        var format = "G";

        MatchToStringWithInvariantCulture(scalar, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void FloatingPoint_MatchToStringWithInvariantCulture(Scalar scalar)
    {
        var format = "F4";

        MatchToStringWithInvariantCulture(scalar, format);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void Null_MatchToStringWithInvariantCulture(Scalar scalar)
    {
        string? format = null;

        MatchToStringWithInvariantCulture(scalar, format);
    }

    private static void MatchToStringWithInvariantCulture(Scalar scalar, string? format)
    {
        var expected = scalar.ToString(format, CultureInfo.InvariantCulture);

        var actual = Target(scalar, format);

        Assert.Equal(expected, actual);
    }
}
