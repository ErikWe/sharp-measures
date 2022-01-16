namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct MetricPrefix(double Scale) :
    IComparable<MetricPrefix>
{
    public static MetricPrefix Yotta { get; } = WithPowerOfTen(24);
    public static MetricPrefix Zetta { get; } = WithPowerOfTen(21);
    public static MetricPrefix Exa { get; } = WithPowerOfTen(18);
    public static MetricPrefix Peta { get; } = WithPowerOfTen(15);
    public static MetricPrefix Tera { get; } = WithPowerOfTen(12);
    public static MetricPrefix Giga { get; } = WithPowerOfTen(9);
    public static MetricPrefix Mega { get; } = WithPowerOfTen(6);
    public static MetricPrefix Kilo { get; } = WithPowerOfTen(3);
    public static MetricPrefix Hecto{ get; }  = WithPowerOfTen(2);
    public static MetricPrefix Deca { get; } = WithPowerOfTen(1);
    public static MetricPrefix Identity { get; } = WithPowerOfTen(0);
    public static MetricPrefix Deci { get; } = WithPowerOfTen(-1);
    public static MetricPrefix Centi { get; } = WithPowerOfTen(-2);
    public static MetricPrefix Milli { get; } = WithPowerOfTen(-3);
    public static MetricPrefix Micro { get; } = WithPowerOfTen(-6);
    public static MetricPrefix Nano { get; } = WithPowerOfTen(-9);
    public static MetricPrefix Pico { get; } = WithPowerOfTen(-12);
    public static MetricPrefix Femto { get; } = WithPowerOfTen(-15);
    public static MetricPrefix Atto { get; } = WithPowerOfTen(-18);
    public static MetricPrefix Zepto { get; } = WithPowerOfTen(-21);
    public static MetricPrefix Yocto { get; } = WithPowerOfTen(-24);

    public int CompareTo(MetricPrefix other) => Scale.CompareTo(other.Scale);
    public override string ToString() => $"{Scale}x";

    private static MetricPrefix WithPowerOfTen(double power) => new(Math.Pow(10, power));

    public static bool operator <(MetricPrefix x, MetricPrefix y) => x.Scale < y.Scale;
    public static bool operator >(MetricPrefix x, MetricPrefix y) => x.Scale > y.Scale;
    public static bool operator <=(MetricPrefix x, MetricPrefix y) => x.Scale <= y.Scale;
    public static bool operator >=(MetricPrefix x, MetricPrefix y) => x.Scale >= y.Scale;
}