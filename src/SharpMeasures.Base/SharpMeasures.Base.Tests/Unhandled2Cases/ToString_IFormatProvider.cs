namespace SharpMeasures.Tests.Unhandled2Cases;

using System;
using System.Globalization;

using Xunit;

public class ToString_IFormatProvider
{
    private static string Target(Unhandled2 vector, IFormatProvider? formatProvider) => vector.ToString(formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void Valid_MatchCustom(Unhandled2 vector)
    {
        var formatProvider = CultureInfo.CurrentCulture;

        MatchCustom(vector, formatProvider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void Null_MatchCustom(Unhandled2 vector)
    {
        IFormatProvider? formatProvider = null;

        MatchCustom(vector, formatProvider);
    }

    private static void MatchCustom(Unhandled2 vector, IFormatProvider? formatProvider)
    {
        var expected = string.Format(formatProvider, "({0}, {1})", vector.X, vector.Y);

        var actual = Target(vector, formatProvider);

        Assert.Equal(expected, actual);
    }
}
