namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfAngle(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfAngle>
{
    public static UnitOfAngle Radian { get; } = new() { BaseScale = 1 };
    public static UnitOfAngle Degree { get; } = Radian with { BaseScale = Math.PI / 180 };
    public static UnitOfAngle ArcMinute { get; } = Degree with { BaseScale = 1d / 60 };
    public static UnitOfAngle ArcSecond { get; } = ArcMinute with { BaseScale = 1d / 60 };
    public static UnitOfAngle Turn { get; } = Radian with { BaseScale = Math.Tau };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfAngle(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfAngle other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfAngle x, UnitOfAngle y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfAngle x, UnitOfAngle y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfAngle x, UnitOfAngle y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfAngle x, UnitOfAngle y) => x.Factor >= y.Factor;
}