namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfTimeSquared(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfTimeSquared>
{
    public static UnitOfTimeSquared From(UnitOfTime unitOfTime) => new(Math.Pow(unitOfTime.Factor, 2));

    public static UnitOfTimeSquared SquareSecond { get; } = From(UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfTimeSquared(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfTimeSquared other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.Factor >= y.Factor;
}