namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfLinearDensity(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfLinearDensity>
{
    public static UnitOfLinearDensity From(UnitOfMass unitOfMass, UnitOfLength unitOfLength) => new(unitOfMass.Factor / unitOfLength.Factor);

    public static UnitOfLinearDensity KilogramPerMetre { get; } = From(UnitOfMass.Kilogram, UnitOfLength.Metre);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfLinearDensity(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfLinearDensity other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfLinearDensity x, UnitOfLinearDensity y) => x.Factor >= y.Factor;
}