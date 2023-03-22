namespace SharpMeasures.Tests.Unhandled3Cases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(Unhandled3 vector, string? format, IFormatProvider? formatProvider) => vector.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void General_Match(Unhandled3 vector)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void FloatingPoint_Match(Unhandled3 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5})";
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NullString_Match(Unhandled3 vector)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NullIFormatProvider_Match(Unhandled3 vector)
    {
        var format = "G";
        IFormatProvider? provider = null;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3))]
    public void NullStringAndIFormatProvider_Match(Unhandled3 vector)
    {
        string? format = null;
        IFormatProvider? provider = null;

        Match(vector, format, provider);
    }

    private static void Match(Unhandled3 vector, string? format, IFormatProvider? formatProvider)
    {
        if (format is "g" or "G" or null)
        {
            format = "({0}, {1}, {2})";
        }

        var expected = string.Format(formatProvider, format, vector.X, vector.Y, vector.Z);

        var actual = Target(vector, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
