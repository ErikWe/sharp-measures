namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfLength(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfLength>
{
    public static UnitOfLength Metre { get; } = new() { BaseScale = 1 };
    public static UnitOfLength Femtometre { get; } = Metre with { Prefix = MetricPrefix.Femto };
    public static UnitOfLength Picometre { get; } = Metre with { Prefix = MetricPrefix.Pico };
    public static UnitOfLength Nanometre { get; } = Metre with { Prefix = MetricPrefix.Nano };
    public static UnitOfLength Micrometre { get; } = Metre with { Prefix = MetricPrefix.Micro };
    public static UnitOfLength Millimetre { get; } = Metre with { Prefix = MetricPrefix.Milli };
    public static UnitOfLength Centimetre { get; } = Metre with { Prefix = MetricPrefix.Centi };
    public static UnitOfLength Decimetre { get; } = Metre with { Prefix = MetricPrefix.Deci };
    public static UnitOfLength Kilometre { get; } = Metre with { Prefix = MetricPrefix.Kilo };

    public static UnitOfLength AstronomicalUnit { get; } = Metre with { BaseScale = 1.495978797 * Math.Pow(10, 11) };
    public static UnitOfLength Lightyear { get; } = Metre with { BaseScale = 9460730472580800 };
    public static UnitOfLength Parsec { get; } = AstronomicalUnit with { BaseScale = 648000 / Math.PI };

    public static UnitOfLength Inch { get; } = Millimetre with { BaseScale = 25.4 };
    public static UnitOfLength Foot { get; } = Inch with { BaseScale = 12 };
    public static UnitOfLength Yard { get; } = Foot with { BaseScale = 3 };
    public static UnitOfLength Mile { get; } = Yard with { BaseScale = 1760 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfLength(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfLength other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfLength x, UnitOfLength y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfLength x, UnitOfLength y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfLength x, UnitOfLength y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfLength x, UnitOfLength y) => x.Factor >= y.Factor;
}