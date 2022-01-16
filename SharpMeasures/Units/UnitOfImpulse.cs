namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfImpulse(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfImpulse>
{
    public static UnitOfImpulse From(UnitOfForce unitOfForce, UnitOfTime unitOfTime) => new(unitOfForce.Factor * unitOfTime.Factor);

    public static UnitOfImpulse NewtonSecond { get; } = From(UnitOfForce.Newton, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfImpulse(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfImpulse other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfImpulse x, UnitOfImpulse y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfImpulse x, UnitOfImpulse y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfImpulse x, UnitOfImpulse y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfImpulse x, UnitOfImpulse y) => x.Factor >= y.Factor;
}