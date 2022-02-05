namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfSolidAngle(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfSolidAngle>
{
    public static UnitOfSolidAngle From(UnitOfAngle unitOfAngle) => new(Math.Pow(unitOfAngle.Factor, 2));

    public static UnitOfSolidAngle Steradian { get; } = new() { BaseScale = 1 };
    public static UnitOfSolidAngle SquareRadian { get; } = From(UnitOfAngle.Radian);
    public static UnitOfSolidAngle SquareDegree { get; } = From(UnitOfAngle.Degree);
    public static UnitOfSolidAngle SquareArcMinute { get; } = From(UnitOfAngle.ArcMinute);
    public static UnitOfSolidAngle SquareArcSecond { get; } = From(UnitOfAngle.ArcSecond);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfSolidAngle(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfSolidAngle other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.Factor >= y.Factor;
}