namespace SharpMeasures.Tests.Unhandled4Cases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(Unhandled4 vector, IFormatProvider? formatProvider) => vector.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void Valid_MatchCustom(Unhandled4 vector)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchCustom(vector, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4))]
    public void Null_MatchCustom(Unhandled4 vector)
    {
        IFormatProvider? formatProvider = null;

        MatchCustom(vector, formatProvider);
    }

    private static void MatchCustom(Unhandled4 vector, IFormatProvider? formatProvider)
    {
        var expected = string.Format(formatProvider, "({0}, {1}, {2}, {3})", vector.X, vector.Y, vector.Z, vector.W);

        var actual = Target(vector, formatProvider);

        Assert.Equal(expected, actual);
    }
}
