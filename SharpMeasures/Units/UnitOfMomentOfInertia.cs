namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfMomentOfInertia(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfMomentOfInertia>
{
    public static UnitOfMomentOfInertia From(UnitOfMass unitOfMass, UnitOfLength unitOfLength) => new(unitOfMass.Factor * Math.Pow(unitOfLength.Factor, 2));

    public static UnitOfMomentOfInertia KilogramMetreSquared { get; } = From(UnitOfMass.Kilogram, UnitOfLength.Metre);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfMomentOfInertia(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfMomentOfInertia other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.Factor >= y.Factor;
}