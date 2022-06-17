namespace SharpMeasures;

using System;

/// <summary>Describes a metric prefix. Common <see cref="MetricPrefix"/> exists as static properties.</summary>
public readonly record struct MetricPrefix :
    IPrefix,
    IComparable<MetricPrefix>
{
    /// <summary>Denotes that the value should be scaled by one septillion [10^24 = (1 000)^8]. Usually written as [Y].</summary>
    public static MetricPrefix Yotta { get; } = ThousandToThePower(8);
    /// <summary>Denotes that the value should be scaled by one sextillion [10^21 = (1 000)^7]. Usually written as [Z].</summary>
    public static MetricPrefix Zetta { get; } = ThousandToThePower(7);
    /// <summary>Denotes that the value should be scaled by one quintillion [10^18 = (1 000)^6]. Usually written as [E].</summary>
    public static MetricPrefix Exa { get; } = ThousandToThePower(6);
    /// <summary>Denotes that the value should be scaled by one quadrillion [10^15 = (1 000)^5]. Usually written as [P].</summary>
    public static MetricPrefix Peta { get; } = ThousandToThePower(5);
    /// <summary>Denotes that the value should be scaled by one trillion [10^12 = (1 000)^4]. Usually written as [T].</summary>
    public static MetricPrefix Tera { get; } = ThousandToThePower(4);
    /// <summary>Denotes that the value should be scaled by one billion [10^9 = (1 000)^3 = 1 000 000 000]. Usually written as [G].</summary>
    public static MetricPrefix Giga { get; } = ThousandToThePower(3);
    /// <summary>Denotes that the value should be scaled by one million [10^6 = (1 000)^2 = 1 000 000]. Usually written as [M].</summary>
    public static MetricPrefix Mega { get; } = ThousandToThePower(2);
    /// <summary>Denotes that the value should be scaled by one thousand [10^3 = 1 000]. Usually written as [k].</summary>
    public static MetricPrefix Kilo { get; } = ThousandToThePower(1);
    /// <summary>Denotes that the value should be scaled by one hundred [10^2 = 100]. Usually written as [h].</summary>
    public static MetricPrefix Hecto { get; } = TenToThePower(2);
    /// <summary>Denotes that the value should be scaled by ten [10^1 = 10]. Usually written as [da].</summary>
    public static MetricPrefix Deca { get; } = TenToThePower(1);
    /// <summary>Denotes that the value should not be scaled, or scaled by one [10^0 = 1].</summary>
    public static MetricPrefix Identity { get; } = new(1);
    /// <summary>Denotes that the value should be scaled by one tenth [10^(-1) = 0.1]. Usually written as [d].</summary>
    public static MetricPrefix Deci { get; } = TenToThePower(-1);
    /// <summary>Denotes that the value should be scaled by one hundreth [10^(-2) = 0.01]. Usually written as [c].</summary>
    public static MetricPrefix Centi { get; } = TenToThePower(-2);
    /// <summary>Denotes that the value should be scaled by one thousandth [10^(-3) = (1 000)^(-1) = 0.001]. Usually written as [m].</summary>
    public static MetricPrefix Milli { get; } = ThousandToThePower(-1);
    /// <summary>Denotes that the value should be scaled by one millionth [10^(-6) = (1 000)^(-2) = 0.000 001]. Usually written as [μ].</summary>
    public static MetricPrefix Micro { get; } = ThousandToThePower(-2);
    /// <summary>Denotes that the value should be scaled by one billionth [10^(-9) = (1 000)^(-3) = 0.000 000 001]. Usually written as [n].</summary>
    public static MetricPrefix Nano { get; } = ThousandToThePower(-3);
    /// <summary>Denotes that the value should be scaled by one trillionth [10^(-12) = (1 000)^(-4)]. Usually written as [p].</summary>
    public static MetricPrefix Pico { get; } = ThousandToThePower(-4);
    /// <summary>Denotes that the value should be scaled by one quadrillionth [10^(-15) = (1 000)^(-5)]. Usually written as [f].</summary>
    public static MetricPrefix Femto { get; } = ThousandToThePower(-5);
    /// <summary>Denotes that the value should be scaled by one quintillionth [10^(-18) = (1 000)^(-6)]. Usually written as [a].</summary>
    public static MetricPrefix Atto { get; } = ThousandToThePower(-6);
    /// <summary>Denotes that the value should be scaled by one sextillionth [10^(-21) = (1 000)^(-7)]. Usually written as [z].</summary>
    public static MetricPrefix Zepto { get; } = ThousandToThePower(-7);
    /// <summary>Denotes that the value should be scaled by one septillionth [10^(-24) = (1 000)^(-8)]. Usually written as [y].</summary>
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
    /// <summary>Produces a description of <see langword="this"/> containing the scale-factor, followed by 'x'.</summary>
    public override string ToString() => $"{Factor.Value}x";

    /// <inheritdoc cref="Scalar.operator &lt;"/>
    public static bool operator <(MetricPrefix x, MetricPrefix y) => x.Factor < y.Factor;
    /// <inheritdoc cref="Scalar.operator &gt;"/>
    public static bool operator >(MetricPrefix x, MetricPrefix y) => x.Factor > y.Factor;
    /// <inheritdoc cref="Scalar.operator &lt;="/>
    public static bool operator <=(MetricPrefix x, MetricPrefix y) => x.Factor <= y.Factor;
    /// <inheritdoc cref="Scalar.operator &gt;="/>
    public static bool operator >=(MetricPrefix x, MetricPrefix y) => x.Factor >= y.Factor;
}
