namespace SharpMeasures;

using System;
using System.Globalization;

/// <summary>Describes a metric prefix. Common <see cref="MetricPrefix"/> exists as static properties.</summary>
public readonly record struct MetricPrefix : IPrefix, IComparable<MetricPrefix>
{
    /// <summary>Indicates that the value should be scaled by one septillion { 10 ^ 24 = 1 000 ^ 8 }. Usually denoted by [Y].</summary>
    public static MetricPrefix Yotta { get; } = ThousandToThePower(8);
    /// <summary>Indicates that the value should be scaled by one sextillion { 10 ^ 21 = 1 000 ^ 7 }. Usually denoted by [Z].</summary>
    public static MetricPrefix Zetta { get; } = ThousandToThePower(7);
    /// <summary>Indicates that the value should be scaled by one quintillion { 10 ^ 18 = 1 000 ^ 6 }. Usually denoted by [E].</summary>
    public static MetricPrefix Exa { get; } = ThousandToThePower(6);
    /// <summary>Indicates that the value should be scaled by one quadrillion { 10 ^ 15 = 1 000 ^ 5 }. Usually denoted by [P].</summary>
    public static MetricPrefix Peta { get; } = ThousandToThePower(5);
    /// <summary>Indicates that the value should be scaled by one trillion { 10 ^ 12 = 1 000 ^ 4 }. Usually denoted by [T].</summary>
    public static MetricPrefix Tera { get; } = ThousandToThePower(4);
    /// <summary>Indicates that the value should be scaled by one billion { 10 ^ 9 = 1 000 ^ 3 = 1 000 000 000 }. Usually denoted by [G].</summary>
    public static MetricPrefix Giga { get; } = ThousandToThePower(3);
    /// <summary>Indicates that the value should be scaled by one million { 10 ^ 6 = 1 000 ^ 2 = 1 000 000 }. Usually denoted by [M].</summary>
    public static MetricPrefix Mega { get; } = ThousandToThePower(2);
    /// <summary>Indicates that the value should be scaled by one thousand { 10 ^ 3 = 1 000 }. Usually denoted by [k].</summary>
    public static MetricPrefix Kilo { get; } = ThousandToThePower(1);
    /// <summary>Indicates that the value should be scaled by one hundred { 10 ^ 2 = 100 }. Usually denoted by [h].</summary>
    public static MetricPrefix Hecto { get; } = TenToThePower(2);
    /// <summary>Indicates that the value should be scaled by ten { 10 ^ 1 = 10 }. Usually denoted by [da].</summary>
    public static MetricPrefix Deca { get; } = TenToThePower(1);
    /// <summary>Indicates that the value should not be scaled.</summary>
    public static MetricPrefix Identity { get; } = new(1);
    /// <summary>Indicates that the value should be scaled by one tenth { 10 ^ -1 = 0.1 }. Usually denoted by [d].</summary>
    public static MetricPrefix Deci { get; } = TenToThePower(-1);
    /// <summary>Indicates that the value should be scaled by one hundreth { 10 ^ -2 = 0.01 }. Usually denoted by [c].</summary>
    public static MetricPrefix Centi { get; } = TenToThePower(-2);
    /// <summary>Indicates that the value should be scaled by one thousandth { 10 ^ -3 = 1 000 ^ -1 = 0.001 }. Usually denoted by [m].</summary>
    public static MetricPrefix Milli { get; } = ThousandToThePower(-1);
    /// <summary>Indicates that the value should be scaled by one millionth { 10 ^ -6 = 1 000 ^ -2 = 0.000 001 }. Usually denoted by [μ].</summary>
    public static MetricPrefix Micro { get; } = ThousandToThePower(-2);
    /// <summary>Indicates that the value should be scaled by one billionth { 10 ^ -9 = 1 000 ^ -3 = 0.000 000 001 }. Usually denoted by [n].</summary>
    public static MetricPrefix Nano { get; } = ThousandToThePower(-3);
    /// <summary>Indicates that the value should be scaled by one trillionth { 10 ^ -12 = 1 000 ^ -4 }. Usually denoted by [p].</summary>
    public static MetricPrefix Pico { get; } = ThousandToThePower(-4);
    /// <summary>Indicates that the value should be scaled by one quadrillionth { 10 ^ -15 = 1 000 ^ -5 }. Usually denoted by [f].</summary>
    public static MetricPrefix Femto { get; } = ThousandToThePower(-5);
    /// <summary>Indicates that the value should be scaled by one quintillionth { 10 ^ -18 = 1 000 ^ -6 }. Usually denoted by [a].</summary>
    public static MetricPrefix Atto { get; } = ThousandToThePower(-6);
    /// <summary>Indicates that the value should be scaled by one sextillionth { 10 ^ -21 = 1 000 ^ -7 }. Usually denoted by [z].</summary>
    public static MetricPrefix Zepto { get; } = ThousandToThePower(-7);
    /// <summary>Indicates that the value should be scaled by one septillionth { 10 ^ -24 = 1 000 ^ -8 }. Usually denoted by [y].</summary>
    public static MetricPrefix Yocto { get; } = ThousandToThePower(-8);

    /// <summary>Constructs a custom <see cref="MetricPrefix"/>, describing a factor { 10 ^ <paramref name="exponent"/> }.</summary>
    /// <param name="exponent">The exponent of { 10 ^ <paramref name="exponent"/> }.</param>
    public static MetricPrefix TenToThePower(Scalar exponent) => new(Math.Pow(10, exponent));

    /// <summary>Constructs a custom <see cref="MetricPrefix"/>, describing a factor { 1 000 ^ <paramref name="exponent"/> }.</summary>
    /// <param name="exponent">The exponent of { 1 000 ^ <paramref name="exponent"/> }.</param>
    public static MetricPrefix ThousandToThePower(Scalar exponent) => new(Math.Pow(1000, exponent));

    /// <inheritdoc/>
    public Scalar Factor { get; }

    /// <summary>Constructs a <see cref="MetricPrefix"/> representing { <paramref name="factor"/> }.</summary>
    /// <param name="factor">The scale-factor represented by the constructed <see cref="MetricPrefix"/>.</param>
    public MetricPrefix(Scalar factor)
    {
        Factor = factor;
    }

    /// <inheritdoc cref="Scalar.CompareTo(Scalar)"/>
    public int CompareTo(MetricPrefix other) => Factor.CompareTo(other.Factor);

    /// <summary>Formats the represented <see cref="Factor"/> using the current culture.</summary>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString()"/>.</remarks>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);
    /// <summary>Formats the represented <see cref="Factor"/> according to <paramref name="format"/>, using the current culture.</summary>
    /// <param name="format">The format.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?)"/>.</remarks>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);
    /// <summary>Formats the represented represented <see cref="Factor"/> using culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    /// <param name="formatProvider">Provides culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(IFormatProvider?)"/>.</remarks>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);
    /// <summary>Formats the represented <see cref="Factor"/> according to <paramref name="format"/>, using culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">Provides culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="double.ToString(string?, IFormatProvider?)"/>.</remarks>
    public string ToString(string? format, IFormatProvider? formatProvider) => Factor.ToString(format, formatProvider);

    /// <inheritdoc cref="Scalar.operator &lt;"/>
    public static bool operator <(MetricPrefix x, MetricPrefix y) => x.Factor < y.Factor;
    /// <inheritdoc cref="Scalar.operator &gt;"/>
    public static bool operator >(MetricPrefix x, MetricPrefix y) => x.Factor > y.Factor;
    /// <inheritdoc cref="Scalar.operator &lt;="/>
    public static bool operator <=(MetricPrefix x, MetricPrefix y) => x.Factor <= y.Factor;
    /// <inheritdoc cref="Scalar.operator &gt;="/>
    public static bool operator >=(MetricPrefix x, MetricPrefix y) => x.Factor >= y.Factor;
}
