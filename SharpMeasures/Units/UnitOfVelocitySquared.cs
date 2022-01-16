namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfVelocitySquared(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfVelocitySquared>
{
    public static UnitOfVelocitySquared From(UnitOfVelocity unitOfVelocity) => new(Math.Pow(unitOfVelocity.Factor, 2));

    public static UnitOfVelocitySquared SquareMetrePerSecondSquared { get; } = From(UnitOfVelocity.MetrePerSecond);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfVelocitySquared(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfVelocitySquared other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfVelocitySquared x, UnitOfVelocitySquared y) => x.Factor >= y.Factor;
}