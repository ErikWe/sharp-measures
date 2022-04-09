namespace SharpMeasures;

using System;

/// <summary>Describes a metric prefix. Common <see cref="MetricPrefix"/> exists as static properties, and <see cref="WithPowerOfTen(double)"/> can be used to construct
/// custom <see cref="MetricPrefix"/>.</summary>
public readonly record struct MetricPrefix :
    IComparable<MetricPrefix>
{
    /// <summary>Denotes that a value should be scaled by one septillion [10^24]. Usually written as [Y].</summary>
    public static MetricPrefix Yotta { get; } = WithPowerOfTen(24);
    /// <summary>Denotes that a value should be scaled by one sextillion [10^21]. Usually written as [Z].</summary>
    public static MetricPrefix Zetta { get; } = WithPowerOfTen(21);
    /// <summary>Denotes that a value should be scaled by one quintillion [10^18]. Usually written as [E].</summary>
    public static MetricPrefix Exa { get; } = WithPowerOfTen(18);
    /// <summary>Denotes that a value should be scaled by one quadrillion [10^15]. Usually written as [P].</summary>
    public static MetricPrefix Peta { get; } = WithPowerOfTen(15);
    /// <summary>Denotes that a value should be scaled by one trillion [10^12]. Usually written as [T].</summary>
    public static MetricPrefix Tera { get; } = WithPowerOfTen(12);
    /// <summary>Denotes that a value should be scaled by one billion [10^9 = 1 000 000 000]. Usually written as [G].</summary>
    public static MetricPrefix Giga { get; } = WithPowerOfTen(9);
    /// <summary>Denotes that a value should be scaled by one million [10^6 = 1 000 000]. Usually written as [M].</summary>
    public static MetricPrefix Mega { get; } = WithPowerOfTen(6);
    /// <summary>Denotes that a value should be scaled by one thousand [10^3 = 1 000]. Usually written as [k].</summary>
    public static MetricPrefix Kilo { get; } = WithPowerOfTen(3);
    /// <summary>Denotes that a value should be scaled by one hundred [10^2 = 100]. Usually written as [h].</summary>
    public static MetricPrefix Hecto { get; } = WithPowerOfTen(2);
    /// <summary>Denotes that a value should be scaled by ten [10^1 = 10]. Written as [da].</summary>
    public static MetricPrefix Deca { get; } = WithPowerOfTen(1);
    /// <summary>Denotes that a value should not be scaled, or scaled by one.</summary>
    /// <remarks>This should, in most cases, be equivalent to indicating no <see cref="MetricPrefix"/>.</remarks>
    public static MetricPrefix Identity { get; } = WithPowerOfTen(0);
    /// <summary>Denotes that a value should be scaled by one tenth [10^(-1) = 0.1]. Usually written as [d].</summary>
    public static MetricPrefix Deci { get; } = WithPowerOfTen(-1);
    /// <summary>Denotes that a value should be scaled by one hundreth [10^(-2) = 0.01]. Usually written as [d].</summary>
    public static MetricPrefix Centi { get; } = WithPowerOfTen(-2);
    /// <summary>Denotes that a value should be scaled by one thousandth [10^(-3) = 0.001]. Usually written as [m].</summary>
    public static MetricPrefix Milli { get; } = WithPowerOfTen(-3);
    /// <summary>Denotes that a value should be scaled by one millionth [10^(-6) = 0.000 001]. Usually written as [μ].</summary>
    public static MetricPrefix Micro { get; } = WithPowerOfTen(-6);
    /// <summary>Denotes that a value should be scaled by one billionth [10^(-9) = 0.000 000 001]. Usually written as [n].</summary>
    public static MetricPrefix Nano { get; } = WithPowerOfTen(-9);
    /// <summary>Denotes that a value should be scaled by one trillionth [10^(-12)]. Usually written as [p].</summary>
    public static MetricPrefix Pico { get; } = WithPowerOfTen(-12);
    /// <summary>Denotes that a value should be scaled by one quadrillionth [10^(-15)]. Usually written as [f].</summary>
    public static MetricPrefix Femto { get; } = WithPowerOfTen(-15);
    /// <summary>Denotes that a value should be scaled by one quintillionth [10^(-18)]. Usually written as [a].</summary>
    public static MetricPrefix Atto { get; } = WithPowerOfTen(-18);
    /// <summary>Denotes that a value should be scaled by one sextillionth [10^(-21)]. Usually written as [z].</summary>
    public static MetricPrefix Zepto { get; } = WithPowerOfTen(-21);
    /// <summary>Denotes that a value should be scaled by one septillionth [10^(-24)]. Usually written as [y].</summary>
    public static MetricPrefix Yocto { get; } = WithPowerOfTen(-24);

    /// <summary>Constructs a custom <see cref="MetricPrefix"/>, with scale 10^(<paramref name="power"/>).</summary>
    /// <param name="power">The scale of the constructed <see cref="MetricPrefix"/> will be ten raised to this power.</param>
    public static MetricPrefix WithPowerOfTen(double power) => new(Math.Pow(10, power));

    /// <summary>The amount that values should be scaled by.</summary>
    public double Scale { get; init; }

    /// <summary>Constructs a new <see cref="MetricPrefix"/> with scale <paramref name="scale"/>.</summary>
    /// <param name="scale">The amount that values should be scaled by.</param>
    public MetricPrefix(Scalar scale) : this(scale.Magnitude) { }

    /// <summary>Constructs a new <see cref="MetricPrefix"/> with scale <paramref name="scale"/>.</summary>
    /// <param name="scale">The amount that values should be scaled by.</param>
    public MetricPrefix(double scale)
    {
        Scale = scale;
    }

    /// <inheritdoc/>
    public int CompareTo(MetricPrefix other) => Scale.CompareTo(other.Scale);
    /// <summary>Produces a formatted string from the scale of the <see cref="MetricPrefix"/> - followed by 'x', to indicate scaling.</summary>
    public override string ToString() => $"{Scale}x";

    /// <summary>Determines whether the scale of <paramref name="x"/> is less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the scale of this <see cref="MetricPrefix"/> is less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the scale of <paramref name="x"/> is less than that of this <see cref="MetricPrefix"/>.</param>
    public static bool operator <(MetricPrefix x, MetricPrefix y) => x.Scale < y.Scale;
    /// <summary>Determines whether the scale of <paramref name="x"/> is greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the scale of this <see cref="MetricPrefix"/> is greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the scale of <paramref name="x"/> is greater than that of this <see cref="MetricPrefix"/>.</param>
    public static bool operator >(MetricPrefix x, MetricPrefix y) => x.Scale > y.Scale;
    /// <summary>Determines whether the scale of <paramref name="x"/> is less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the scale of this <see cref="MetricPrefix"/> is less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the scale of <paramref name="x"/> is less than or equal to that of this <see cref="MetricPrefix"/>.</param>
    public static bool operator <=(MetricPrefix x, MetricPrefix y) => x.Scale <= y.Scale;
    /// <summary>Determines whether the scale of <paramref name="x"/> is greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the scale of this <see cref="MetricPrefix"/> is greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the scale of <paramref name="x"/> is greater than or equal to that of this <see cref="MetricPrefix"/>.</param>
    public static bool operator >=(MetricPrefix x, MetricPrefix y) => x.Scale >= y.Scale;
}