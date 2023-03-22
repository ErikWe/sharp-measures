namespace SharpMeasures.Tests.Vector3Cases;

using System;
using System.Globalization;

using Xunit;

public class ToString_String_IFormatProvider
{
    private static string Target(Vector3 vector, string? format, IFormatProvider? formatProvider) => vector.ToString(format, formatProvider);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void General_Match(Vector3 vector)
    {
        var format = "G";
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void FloatingPoint_Match(Vector3 vector)
    {
        var format = "({0:F4}, {1:F3}, {2:F5})";
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void NullString_Match(Vector3 vector)
    {
        string? format = null;
        var provider = CultureInfo.CurrentCulture;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void NullIFormatProvider_Match(Vector3 vector)
    {
        var format = "G";
        IFormatProvider? provider = null;

        Match(vector, format, provider);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void NullStringAndIFormatProvider_Match(Vector3 vector)
    {
        string? format = null;
        IFormatProvider? provider = null;

        Match(vector, format, provider);
    }

    private static void Match(Vector3 vector, string? format, IFormatProvider? formatProvider)
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
