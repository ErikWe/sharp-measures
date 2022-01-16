namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfYank(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfYank>
{
    public static UnitOfYank From(UnitOfForce unitOfForce, UnitOfTime unitOfTime) => new(unitOfForce.Factor / unitOfTime.Factor);

    public static UnitOfYank NewtonPerSecond { get; } = From(UnitOfForce.Newton, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfYank(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfYank other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfYank x, UnitOfYank y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfYank x, UnitOfYank y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfYank x, UnitOfYank y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfYank x, UnitOfYank y) => x.Factor >= y.Factor;
}