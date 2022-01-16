namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfMass(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfMass>
{
    public static UnitOfMass Gram { get; } = new() { BaseScale = 1d / 1000 };

    public static UnitOfMass Milligram { get; } = Gram with { Prefix = MetricPrefix.Milli };
    public static UnitOfMass Hectogram { get; } = Gram with { Prefix = MetricPrefix.Hecto };
    public static UnitOfMass Kilogram { get; } = Gram with { Prefix = MetricPrefix.Kilo };
    public static UnitOfMass Tonne { get; } = Gram with { Prefix = MetricPrefix.Mega };

    /// <summary>Avoirdupois ounce (US customary and British imperial), abbreviated (oz). Equivalent to 28.349523125 <see cref="Gram"/>.</summary>
    public static UnitOfMass Ounce { get; } = Gram with { BaseScale = 28.349523125 };
    /// <summary>Avoirdupois pound (US customary and British imperial), abbreviated (lb). Equivalent to 16 <see cref="Ounce"/>.</summary>
    public static UnitOfMass Pound { get; } = Ounce with { BaseScale = 16 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfMass(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfMass other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfMass x, UnitOfMass y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfMass x, UnitOfMass y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfMass x, UnitOfMass y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfMass x, UnitOfMass y) => x.Factor >= y.Factor;
}