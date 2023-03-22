namespace SharpMeasures.Tests.Vector2Cases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(Vector2 vector, string? format, IFormatProvider? formatProvider) => vector.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void General_Match(Vector2 vector)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void FloatingPoint_Match(Vector2 vector)
    {
        var format = "({0:F4}, {1:F3})";
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void NullString_Match(Vector2 vector)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void NullIFormatProvider_Match(Vector2 vector)
    {
        var format = "G";
        IFormatProvider? provider = null;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void NullStringAndIFormatProvider_Match(Vector2 vector)
    {
        string? format = null;
        IFormatProvider? provider = null;

        Match(vector, format, provider);
    }

    private static void Match(Vector2 vector, string? format, IFormatProvider? formatProvider)
    {
        if (format is "g" or "G" or null)
        {
            format = "({0}, {1})";
        }

        var expected = string.Format(formatProvider, format, vector.X, vector.Y);

        var actual = Target(vector, format, formatProvider);

        Assert.Equal(expected, actual);
    }
}
