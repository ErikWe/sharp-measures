namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfForce(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfForce>
{
    public static UnitOfForce From(UnitOfMass unitOfMass, UnitOfAcceleration unitOfAcceleration) => new(unitOfMass.Factor * unitOfAcceleration.Factor);

    public static UnitOfForce Newton { get; } = From(UnitOfMass.Kilogram, UnitOfAcceleration.MetrePerSecondSquared);
    public static UnitOfForce PoundForce { get; } = From(UnitOfMass.Pound, UnitOfAcceleration.StandardGravity);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfForce(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfForce other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfForce x, UnitOfForce y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfForce x, UnitOfForce y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfForce x, UnitOfForce y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfForce x, UnitOfForce y) => x.Factor >= y.Factor;
}