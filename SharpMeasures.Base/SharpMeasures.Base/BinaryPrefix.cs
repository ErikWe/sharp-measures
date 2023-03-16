namespace SharpMeasures;

using System;
using System.Globalization;

/// <summary>Describes a metric prefix, scaling quantities using powers of 2.</summary>
/// <remarks>Common <see cref="BinaryPrefix"/> are defined as static properties.</remarks>
public sealed record class BinaryPrefix : IPrefix, IComparable<BinaryPrefix>, IFormattable
{
    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 80 = 1 024 ^ 8 }. Usually denoted by { Yi }.</summary>
    public static BinaryPrefix Yobi { get; } = ThousandTwentyFourToThePower(8);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 70 = 1 024 ^ 7 }. Usually denoted by { Zi }.</summary>
    public static BinaryPrefix Zebi { get; } = ThousandTwentyFourToThePower(7);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 60 = 1 024 ^ 6 }. Usually denoted by { Ei }.</summary>
    public static BinaryPrefix Exbi { get; } = ThousandTwentyFourToThePower(6);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 50 = 1 024 ^ 5 }. Usually denoted by { Pi }.</summary>
    public static BinaryPrefix Pebi { get; } = ThousandTwentyFourToThePower(5);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 40 = 1 024 ^ 4 }. Usually denoted by { Ti }.</summary>
    public static BinaryPrefix Tebi { get; } = ThousandTwentyFourToThePower(4);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 30 = 1 024 ^ 3 }. Usually denoted by { Gi }.</summary>
    public static BinaryPrefix Gibi { get; } = ThousandTwentyFourToThePower(3);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 20 = 1 024 ^ 2 = 1 048 576 }. Usually denoted by { Mi }.</summary>
    public static BinaryPrefix Mebi { get; } = ThousandTwentyFourToThePower(2);

    /// <summary>Indicates that a quantity should be scaled by { 2 ^ 10 = 1 024 }. Usually denoted by { Ki }.</summary>
    public static BinaryPrefix Kibi { get; } = ThousandTwentyFourToThePower(1);

    /// <summary>Indicates that a quantity should not be scaled, or scaled by { 1 }.</summary>
    public static BinaryPrefix Identity { get; } = new(1);

    /// <summary>Scales any quantity to { 0 }.</summary>
    public static BinaryPrefix Zero { get; } = new(0);

    /// <summary>Constructs a <see cref="BinaryPrefix"/>, representing a scale-factor of { 2 } raised to the provided power.</summary>
    /// <param name="exponent">The <see cref="Scalar"/> power, used as the exponent when computing the scale-factor.</param>
    /// <returns>The constructed <see cref="BinaryPrefix"/>, representing the scale-factor { 2 ^ <paramref name="exponent"/> }.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static BinaryPrefix TwoToThePower(Scalar exponent) => new(Math.Pow(2, exponent));

    /// <summary>Constructs a <see cref="BinaryPrefix"/>, representing a scale-factor of { 1 024 } raised to the provided power.</summary>
    /// <param name="exponent">The <see cref="Scalar"/> power, used as the exponent when computing the scale-factor.</param>
    /// <returns>The constructed <see cref="BinaryPrefix"/>, representing the scale-factor { 1 024 ^ <paramref name="exponent"/> }.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static BinaryPrefix ThousandTwentyFourToThePower(Scalar exponent) => new(Math.Pow(1024, exponent));

    /// <summary>The scale-factor of the <see cref="BinaryPrefix"/>.</summary>
    public Scalar Factor { get; }

    /// <summary>Instantiates a <see cref="BinaryPrefix"/>, representing a <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The <see cref="Scalar"/> factor of the constructed <see cref="BinaryPrefix"/>.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public BinaryPrefix(Scalar factor)
    {
        if (factor.IsNaN)
        {
            throw new ArgumentException($"The scale-factor of a {nameof(BinaryPrefix)} must be defined.", nameof(factor));
        }

        if (factor.IsInfinite)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), factor, $"The scale-factor of a {nameof(BinaryPrefix)} must be finite.");
        }

        Factor = factor;
    }

    /// <summary>Compares the <see cref="BinaryPrefix"/> to another, provided, <see cref="BinaryPrefix"/>, resulting in:
    /// <list type="bullet">
    /// <item><term>1</term><description> The <see cref="BinaryPrefix"/> represents a larger scale-factor than does the provided <see cref="BinaryPrefix"/>, or the provided <see cref="BinaryPrefix"/> is <see langword="null"/>.</description></item>
    /// <item><term>0</term><description> The <see cref="BinaryPrefix"/> and the provided <see cref="BinaryPrefix"/> represent the same larger scale-factor.</description></item>
    /// <item><term>-1</term><description> The <see cref="BinaryPrefix"/> represents a smaller larger scale-factor than does the provided <see cref="BinaryPrefix"/>.</description></item>
    /// </list></summary>
    /// <param name="other">The <see cref="BinaryPrefix"/> to which the original <see cref="BinaryPrefix"/> is compared.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.CompareTo(Scalar)"/>.</remarks>
    /// <returns>One of the <see cref="int"/> values { 1, 0, -1 }, as detailed above.</returns>
    public int CompareTo(BinaryPrefix? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Factor.CompareTo(other.Factor);
    }

    /// <summary>Determines whether the <see cref="BinaryPrefix"/> is equivalent to another, provided, <see cref="BinaryPrefix"/>.</summary>
    /// <param name="other">The <see cref="BinaryPrefix"/> to which the original <see cref="BinaryPrefix"/> is compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the two <see cref="BinaryPrefix"/> are equivalent.</returns>
    public bool Equals(BinaryPrefix? other) => other is not null && Factor.Equals(other.Factor);

    /// <summary>Determines whether the provided <see cref="BinaryPrefix"/> are equivalent.</summary>
    /// <param name="lhs">The first of the two <see cref="BinaryPrefix"/> that are compared.</param>
    /// <param name="rhs">The second of the two <see cref="BinaryPrefix"/> that are compared.</param>
    /// <returns>A <see cref="bool"/> indicating whether the provided <see cref="BinaryPrefix"/> are equivalent.</returns>
    public static bool Equals(BinaryPrefix? lhs, BinaryPrefix? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Computes the <see cref="int"/> hash code describing the <see cref="BinaryPrefix"/>.</summary>
    /// <returns>The <see cref="int"/> hash code describing the <see cref="BinaryPrefix"/>.</returns>
    public override int GetHashCode() => Factor.GetHashCode();

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>, formatted according to the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString()"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>.</returns>
    public override string ToString() => ToString(CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>, formatted according to the provided <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString(IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.CurrentCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString(string?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>, formatted according to the provided <see cref="string"/> and <see cref="IFormatProvider"/>.</summary>
    /// <param name="formatProvider">The <see cref="IFormatProvider"/>, providing culture-specific formatting information.</param>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToString(string?, IFormatProvider?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => Factor.ToString(format, formatProvider);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>, formatted according to the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToStringInvariant()"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>.</returns>
    public string ToStringInvariant() => ToString(CultureInfo.InvariantCulture);

    /// <summary>Produces a <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>, formatted according to the provided <see cref="string"/> and the <see cref="CultureInfo.InvariantCulture"/>.</summary>
    /// <param name="format">The <see cref="string"/>, providing formatting information.</param>
    /// <remarks>The behaviour is consistent with <see cref="Scalar.ToStringInvariant(string?)"/>.</remarks>
    /// <returns>A <see cref="string"/>-representation of the <see cref="BinaryPrefix"/>.</returns>
    public string ToStringInvariant(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>Determines whether a <see cref="BinaryPrefix"/>, <paramref name="lhs"/>, represents a smaller scale-factor than another <see cref="BinaryPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="BinaryPrefix"/>, assumed to represent a smaller scale-factor than the other <see cref="BinaryPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="BinaryPrefix"/>, assumed to represent a greater scale-factor than the other <see cref="BinaryPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &lt; <paramref name="rhs"/> }.</remarks>
    public static bool operator <(BinaryPrefix? lhs, BinaryPrefix? rhs) => lhs?.Factor < rhs?.Factor;

    /// <summary>Determines whether a <see cref="BinaryPrefix"/>, <paramref name="lhs"/>, represents a greater scale-factor than another <see cref="BinaryPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="BinaryPrefix"/>, assumed to represent a greater scale-factor than the other <see cref="BinaryPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="BinaryPrefix"/>, assumed to represent a smaller scale-factor than the other <see cref="BinaryPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> &gt; <paramref name="rhs"/> }.</remarks>
    public static bool operator >(BinaryPrefix? lhs, BinaryPrefix? rhs) => lhs?.Factor > rhs?.Factor;

    /// <summary>Determines whether a <see cref="BinaryPrefix"/>, <paramref name="lhs"/>, represents a smaller or equivalent scale-factor compared to another <see cref="BinaryPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="BinaryPrefix"/>, assumed to represent a smaller or equivalent scale-factor compared to the other <see cref="BinaryPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="BinaryPrefix"/>, assumed to represent a greater or equivalent scale-factor compared to the other <see cref="BinaryPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≤ <paramref name="rhs"/> }.</remarks>
    public static bool operator <=(BinaryPrefix? lhs, BinaryPrefix? rhs) => lhs?.Factor <= rhs?.Factor;

    /// <summary>Determines whether a <see cref="BinaryPrefix"/>, <paramref name="lhs"/>, represents a greater or equivalent scale-factor compared to another <see cref="BinaryPrefix"/>, <paramref name="rhs"/>.</summary>
    /// <param name="lhs">The first <see cref="BinaryPrefix"/>, assumed to represent a greater or equivalent scale-factor compared to the other <see cref="BinaryPrefix"/>.</param>
    /// <param name="rhs">The second <see cref="BinaryPrefix"/>, assumed to represent a smaller or equivalent scale-factor compared to the other <see cref="BinaryPrefix"/>.</param>
    /// <remarks>A <see cref="bool"/> representing the truthfulness of { <paramref name="lhs"/> ≥ <paramref name="rhs"/> }.</remarks>
    public static bool operator >=(BinaryPrefix? lhs, BinaryPrefix? rhs) => lhs?.Factor >= rhs?.Factor;

    /// <summary>Constructs a <see cref="BinaryPrefix"/>, representing a <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The scale-factor represented by the constructed <see cref="BinaryPrefix"/>.</param>
    /// <returns>The constructed <see cref="BinaryPrefix"/>, representing the provided <see cref="Scalar"/> factor.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static BinaryPrefix FromScalar(Scalar factor) => new(factor);

    /// <summary>Retrieves the <see cref="Scalar"/> factor of the <see cref="BinaryPrefix"/>.</summary>
    /// <returns>The <see cref="Scalar"/> factor of the <see cref="BinaryPrefix"/>.</returns>
    public Scalar ToScalar() => Factor;

    /// <summary>Constructs a <see cref="BinaryPrefix"/>, representing a <see cref="Scalar"/> factor.</summary>
    /// <param name="factor">The scale-factor represented by the constructed <see cref="BinaryPrefix"/>.</param>
    /// <returns>The constructed <see cref="BinaryPrefix"/>, representing the provided <see cref="Scalar"/> factor.</returns>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static explicit operator BinaryPrefix(Scalar factor) => FromScalar(factor);

    /// <summary>Retrieves the <see cref="Scalar"/> factor of the provided <see cref="BinaryPrefix"/>.</summary>
    /// <param name="metricPrefix">The <see cref="BinaryPrefix"/>, from which the <see cref="Scalar"/> factor is retrieved.</param>
    /// <returns>The <see cref="Scalar"/> factor of the provided <see cref="BinaryPrefix"/>.</returns>
    public static explicit operator Scalar(BinaryPrefix metricPrefix)
    {
        ArgumentNullException.ThrowIfNull(metricPrefix);

        return metricPrefix.ToScalar();
    }
}
