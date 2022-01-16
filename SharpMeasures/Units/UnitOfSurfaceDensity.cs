namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfSurfaceDensity(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfSurfaceDensity>
{
    public static UnitOfSurfaceDensity From(UnitOfMass unitOfMass, UnitOfArea unitOfArea) => new(unitOfMass.Factor / unitOfArea.Factor);

    public static UnitOfSurfaceDensity KilogramPerSquareMetre { get; } = From(UnitOfMass.Kilogram, UnitOfArea.SquareMetre);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfSurfaceDensity(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfSurfaceDensity other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfSurfaceDensity x, UnitOfSurfaceDensity y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfSurfaceDensity x, UnitOfSurfaceDensity y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfSurfaceDensity x, UnitOfSurfaceDensity y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfSurfaceDensity x, UnitOfSurfaceDensity y) => x.Factor >= y.Factor;
}