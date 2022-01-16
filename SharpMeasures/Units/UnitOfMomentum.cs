namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfMomentum(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfMomentum>
{
    public static UnitOfMomentum From(UnitOfMass unitOfMass, UnitOfVelocity unitOfSpeed) => new(unitOfMass.Factor * unitOfSpeed.Factor);

    public static UnitOfMomentum KilogramMetrePerSecond { get; } = From(UnitOfMass.Kilogram, UnitOfVelocity.MetrePerSecond);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfMomentum(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfMomentum other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfMomentum x, UnitOfMomentum y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfMomentum x, UnitOfMomentum y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfMomentum x, UnitOfMomentum y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfMomentum x, UnitOfMomentum y) => x.Factor >= y.Factor;
}