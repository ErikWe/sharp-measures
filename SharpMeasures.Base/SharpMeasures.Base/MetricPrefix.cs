namespace SharpMeasures;

using System;
using System.Globalization;

/// <summary>Describes a metric prefix, scaling quantities using powers of 10.</summary>
/// <remarks>Common <see cref="MetricPrefix"/> are defined as static properties.</remarks>
public sealed record class MetricPrefix : IPrefix, IComparable<MetricPrefix>, IFormattable
{
    /// <summary>Indicates that a quantity should be scaled by one septillion { 10 ^ 24 = 1 000 ^ 8 }. Usually denoted by { Y }.</summary>
    public static MetricPrefix Yotta { get; } = ThousandToThePower(8);

    /// <summary>Indicates that a quantity should be scaled by one sextillion { 10 ^ 21 = 1 000 ^ 7 }. Usually denoted by { Z }.</summary>
    public static MetricPrefix Zetta { get; } = ThousandToThePower(7);

    /// <summary>Indicates that a quantity should be scaled by one quintillion { 10 ^ 18 = 1 000 ^ 6 }. Usually denoted by { E }.</summary>
    public static MetricPrefix Exa { get; } = ThousandToThePower(6);

    /// <summary>Indicates that a quantity should be scaled by one quadrillion { 10 ^ 15 = 1 000 ^ 5 }. Usually denoted by { P }.</summary>
    public static MetricPrefix Peta { get; } = ThousandToThePower(5);

    /// <summary>Indicates that a quantity should be scaled by one trillion { 10 ^ 12 = 1 000 ^ 4 }. Usually denoted by { T }.</summary>
    public static MetricPrefix Tera { get; } = ThousandToThePower(4);

    /// <summary>Indicates that a quantity should be scaled by one billion { 10 ^ 9 = 1 000 ^ 3 = 1 000 000 000 }. Usually denoted by { G }.</summary>
    public static MetricPrefix Giga { get; } = ThousandToThePower(3);

    /// <summary>Indicates that a quantity should be scaled by one million { 10 ^ 6 = 1 000 ^ 2 = 1 000 000 }. Usually denoted by { M }.</summary>
    public static MetricPrefix Mega { get; } = ThousandToThePower(2);

    /// <summary>Indicates that a quantity should be scaled by one thousand { 10 ^ 3 = 1 000 }. Usually denoted by { k }.</summary>
    public static MetricPrefix Kilo { get; } = ThousandToThePower(1);

    /// <summary>Indicates that a quantity should be scaled by one hundred { 10 ^ 2 = 100 }. Usually denoted by { h }.</summary>
    public static MetricPrefix Hecto { get; } = TenToThePower(2);

    /// <summary>Indicates that a quantity should be scaled by ten { 10 ^ 1 = 10 }. Usually denoted by { da }.</summary>
    public static MetricPrefix Deca { get; } = TenToThePower(1);

    /// <summary>Indicates that a quantity should not be scaled, or scaled by { 1 }.</summary>
    public static MetricPrefix Identity { get; } = new(1);

    /// <summary>Scales any quantity to { 0 }.</summary>
    public static MetricPrefix Zero { get; } = new(0);

    /// <summary>Indicates that a quantity should be scaled by one tenth { 10 ^ -1 = 0.1 }. Usually denoted by { d }.</summary>
    public static MetricPrefix Deci { get; } = TenToThePower(-1);

    /// <summary>Indicates that a quantity should be scaled by one hundreth { 10 ^ -2 = 0.01 }. Usually denoted by { c }.</summary>
    public static MetricPrefix Centi { get; } = TenToThePower(-2);

    /// <summary>Indicates that a quantity should be scaled by one thousandth { 10 ^ -3 = 1 000 ^ -1 = 0.001 }. Usually denoted by { m }.</summary>
    public static MetricPrefix Milli { get; } = ThousandToThePower(-1);

    /// <summary>Indicates that a quantity should be scaled by one millionth { 10 ^ -6 = 1 000 ^ -2 = 0.000 001 }. Usually denoted by { μ }.</summary>
    public static MetricPrefix Micro { get; } = ThousandToThePower(-2);

    /// <summary>Indicates that a quantity should be scaled by one billionth { 10 ^ -9 = 1 000 ^ -3 = 0.000 000 001 }. Usually denoted by { n }.</summary>
    public static MetricPrefix Nano { get; } = ThousandToThePower(-3);

    /// <summary>Indicates that a quantity should be scaled by one trillionth { 10 ^ -12 = 1 000 ^ -4 }. Usually denoted by { p }.</summary>
    public static MetricPrefix Pico { get; } = ThousandToThePower(-4);

    /// <summary>Indicates that a quantity should be scaled by one quadrillionth { 10 ^ -15 = 1 000 ^ -5 }. Usually denoted by { f }.</summary>
    public static MetricPrefix Femto { get; } = ThousandToThePower(-5);

    /// <summary>Indicates that a quantity should be scaled by one quintillionth { 10 ^ -18 = 1 000 ^ -6 }. Usually denoted by { a }.</summary>
    public static MetricPrefix Atto { get; } = ThousandToThePower(-6);

    /// <summary>Indicates that a quantity should be scaled by one sextillionth { 10 ^ -21 = 1 000 ^ -7 }. Usually denoted by { z }.</summary>
    public static MetricPrefix Zepto { get; } = ThousandToThePower(-7);

    /// <summary>Indicates that a quantity should be scaled by one septillionth { 10 ^ -24 = 1 000 ^ -8 }. Usually denoted by { y }.</summary>
    public static MetricPrefix Yocto { get; } = ThousandToThePower(-8);

    /// <summary>Constructs a <see cref="MetricPrefix"/>, representing a scale-factor of { 10 } raised to the provided power.</summary>
    /// <param name="exponent">The <see cref="Scalar"/> power, used as the exponent when computing the scale-factor.</param>
    /// <returns>The constructed <see cref="MetricPrefix"/>, representing the scale-factor { 10 ^ <paramref name="exponent"/> }.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static MetricPrefix TenToThePower(Scalar exponent) => new(Math.Pow(10, exponent));

    /// <summary>Constructs a <see cref="MetricPrefix"/>, representing a scale-factor of { 1 000 } raised to the provided power.</summary>
    /// <param name="exponent">The <see cref="Scalar"/> power, used as the exponent when computing the scale-factor.</param>
    /// <returns>The constructed <see cref="MetricPrefix"/>, representing the scale-factor { 1 000 ^ <paramref name="exponent"/> }.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static MetricPrefix ThousandToThePower(Scalar exponent) => new(Math.Pow(1000, exponent));

    /// <summary>The scale-factor of the <see cref="MetricPrefix"/>.</summary>
    public Scalar Factor { get; }

    /// <summary>Instantiates a <see cref="MetricPrefix"/>, representing a <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The <see cref="Scalar"/> factor of the constructed <see cref="MetricPrefix"/>.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public MetricPrefix(Scalar factor)
    {
        if (factor.IsNaN)
        {
            throw new ArgumentException($"The scale-factor of a {nameof(MetricPrefix)} must be defined.", nameof(factor));
        }

        if (factor.IsInfinite)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), factor, $"The scale-factor of a {nameof(MetricPrefix)} must be finite.");
        }

        Factor = factor;
    }

    /// <summary>Compares the <see cref="MetricPrefix"/> to another, provided, <see cref="MetricPrefix"/>, resulting in:
    /// <list type="bullet">
    /// <item><term>1</term><description> The <see cref="MetricPrefix"/> represents a larger scale-factor than does the provided <see cref="MetricPrefix"/>, or the provided <see cref="MetricPrefix"/> is <see langword="null"/>.</description></item>
    /// <item><term>0</term><description> The <see cref="MetricPrefix"/> and the provided <see cref="MetricPrefix"/> represent the same larger scale-factor.</description></item>
    /// <item><term>-1</term><description> The <see cref="MetricPrefix"/> represents a smaller larger scale-factor than does the provided <see cref="MetricPrefix"/>.</description></item>
    /// </list></summary>
    /// <param name="other">The <see cref="MetricPrefix"/> to which the original <see cref="MetricPrefix"/> is compared.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.CompareTo(Scalar)"/>.</remarks>
    /// <returns>One of the <see cref="int"/> values { 1, 0, -1 }, as detailed above.</returns>
    public int CompareTo(MetricPrefix? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Factor.CompareTo(other.Factor);
    }

    /// <summary>Determines whether the <see cref="MetricPrefix"/> is equivalent to another, provided, <see cref="MetricPrefix"/>.</summary>
    /// <param name="other">The <see cref="MetricPrefix"/> to which the original <see cref="MetricPrefix"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="MetricPrefix"/> are equivalent.</returns>
    public bool Equals(MetricPrefix? other) => other is not null && Factor.Equals(other.Factor);

    /// <summary>Determines whether the provided <see cref="MetricPrefix"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="MetricPrefix"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="MetricPrefix"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the provided <see cref="MetricPrefix"/> are equivalent.</returns>
    public static bool Equals(MetricPrefix? lhs, MetricPrefix? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="MetricPrefix"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="MetricPrefix"/>.</returns>
    public override int GetHashCode() => Factor.GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="MetricPrefix"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString()"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="MetricPrefix"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="MetricPrefix"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString(IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="MetricPrefix"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="MetricPrefix"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString(string?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="MetricPrefix"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="MetricPrefix"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString(string?, IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="MetricPrefix"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => Factor.ToString(format, formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="MetricPrefix"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToStringInvariant()"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="MetricPrefix"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="MetricPrefix"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToStringInvariant(string?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="MetricPrefix"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Determines whether a <see cref="MetricPrefix"/>, <paramref name="lhs"/>, represents a smaller scale-factor than another <see cref="MetricPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="MetricPrefix"/>, assumed to represent a smaller scale-factor than the other <see cref="MetricPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="MetricPrefix"/>, assumed to represent a greater scale-factor than the other <see cref="MetricPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &lt; <paramref name="rhs"/> }.</remarks>
    public static bool operator <(MetricPrefix? lhs, MetricPrefix? rhs) => lhs?.Factor < rhs?.Factor;

    /// <summary>Determines whether a <see cref="MetricPrefix"/>, <paramref name="lhs"/>, represents a greater scale-factor than another <see cref="MetricPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="MetricPrefix"/>, assumed to represent a greater scale-factor than the other <see cref="MetricPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="MetricPrefix"/>, assumed to represent a smaller scale-factor than the other <see cref="MetricPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &gt; <paramref name="rhs"/> }.</remarks>
    public static bool operator >(MetricPrefix? lhs, MetricPrefix? rhs) => lhs?.Factor > rhs?.Factor;

    /// <summary>Determines whether a <see cref="MetricPrefix"/>, <paramref name="lhs"/>, represents a smaller or equivalent scale-factor compared to another <see cref="MetricPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="MetricPrefix"/>, assumed to represent a smaller or equivalent scale-factor compared to the other <see cref="MetricPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="MetricPrefix"/>, assumed to represent a greater or equivalent scale-factor compared to the other <see cref="MetricPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≤ <paramref name="rhs"/> }.</remarks>
    public static bool operator <=(MetricPrefix? lhs, MetricPrefix? rhs) => lhs?.Factor <= rhs?.Factor;

    /// <summary>Determines whether a <see cref="MetricPrefix"/>, <paramref name="lhs"/>, represents a greater or equivalent scale-factor compared to another <see cref="MetricPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="MetricPrefix"/>, assumed to represent a greater or equivalent scale-factor compared to the other <see cref="MetricPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="MetricPrefix"/>, assumed to represent a smaller or equivalent scale-factor compared to the other <see cref="MetricPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≥ <paramref name="rhs"/> }.</remarks>
    public static bool operator >=(MetricPrefix? lhs, MetricPrefix? rhs) => lhs?.Factor >= rhs?.Factor;

    /// <summary>Constructs a <see cref="MetricPrefix"/>, representing a <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The scale-factor represented by the constructed <see cref="MetricPrefix"/>.</param>
    /// <returns>The constructed <see cref="MetricPrefix"/>, representing the provided <see cref="Scalar"/> factor.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static MetricPrefix FromScalar(Scalar factor) => new(factor);

    /// <summary>Retrieves the <see cref="Scalar"/> factor of the <see cref="MetricPrefix"/>.</summary>
    /// <returns>The <see cref="Scalar"/> factor of the <see cref="MetricPrefix"/>.</returns>
    public Scalar ToScalar() => Factor;

    /// <summary>Constructs a <see cref="MetricPrefix"/>, representing a <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The scale-factor represented by the constructed <see cref="MetricPrefix"/>.</param>
    /// <returns>The constructed <see cref="MetricPrefix"/>, representing the provided <see cref="Scalar"/> factor.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static explicit operator MetricPrefix(Scalar factor) => FromScalar(factor);

    /// <summary>Retrieves the <see cref="Scalar"/> factor of the provided <see cref="MetricPrefix"/>.</summary>
    /// <param name="metricPrefix">The <see cref="MetricPrefix"/>, from which the <see cref="Scalar"/> factor is retrieved.</param>
    /// <returns>The <see cref="Scalar"/> factor of the provided <see cref="MetricPrefix"/>.</returns>
    public static explicit operator Scalar(MetricPrefix metricPrefix)
    {
        ArgumentNullException.ThrowIfNull(metricPrefix);

        return metricPrefix.ToScalar();
    }
}
