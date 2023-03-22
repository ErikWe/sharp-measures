namespace SharpMeasures.Tests.ScalarCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(Scalar scalar, IFormatProvider? formatProvider) => scalar.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void Valid_MatchToDoubleToString(Scalar scalar)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchToDoubleToString(scalar, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void Null_MatchToDoubleToString(Scalar scalar)
    {
        IFormatProvider? formatProvider = null;

        MatchToDoubleToString(scalar, formatProvider);
    }

    private static void MatchToDoubleToString(Scalar scalar, IFormatProvider? formatProvider)
    {
        var expected = scalar.ToDouble().ToString(formatProvider);

        var actual = Target(scalar, formatProvider);

        Assert.Equal(expected, actual);
    }
}
