namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfSpecificVolume(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfSpecificVolume>
{
    public static UnitOfSpecificVolume From(UnitOfVolume unitOfVolume, UnitOfMass unitOfMass) => new(unitOfVolume.Factor / unitOfMass.Factor);

    public static UnitOfSpecificVolume CubicMetrePerKilogram { get; } = From(UnitOfVolume.CubicMetre, UnitOfMass.Kilogram);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfSpecificVolume(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfSpecificVolume other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfSpecificVolume x, UnitOfSpecificVolume y) => x.Factor >= y.Factor;
}