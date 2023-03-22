namespace SharpMeasures.Tests.BinaryPrefixCases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(BinaryPrefix prefix, IFormatProvider? formatProvider) => prefix.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Valid_MatchFactorToString(BinaryPrefix prefix)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchFactorToString(prefix, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidBinaryPrefix))]
    public void Null_MatchFactorToString(BinaryPrefix prefix)
    {
        IFormatProvider? formatProvider = null;

        MatchFactorToString(prefix, formatProvider);
    }

    private static void MatchFactorToString(BinaryPrefix prefix, IFormatProvider? formatProvider)
    {
        var expected = prefix.Factor.ToString(formatProvider);

        var actual = Target(prefix, formatProvider);

        Assert.Equal(expected, actual);
    }
}
