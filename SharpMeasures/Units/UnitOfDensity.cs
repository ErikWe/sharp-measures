namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfDensity(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfDensity>
{
    public static UnitOfDensity From(UnitOfMass unitOfMass, UnitOfVolume unitOfVolume) => new(unitOfMass.Factor / unitOfVolume.Factor);

    public static UnitOfDensity KilogramPerCubicMetre { get; } = From(UnitOfMass.Kilogram, UnitOfVolume.CubicMetre);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfDensity(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfDensity other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfDensity x, UnitOfDensity y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfDensity x, UnitOfDensity y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfDensity x, UnitOfDensity y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfDensity x, UnitOfDensity y) => x.Factor >= y.Factor;
}