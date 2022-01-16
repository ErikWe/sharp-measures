namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfSolidAngle(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfSolidAngle>
{
    public static UnitOfSolidAngle Steradian { get; } = new() { BaseScale = 1 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfSolidAngle(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfSolidAngle other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor >= y.Factor;
}