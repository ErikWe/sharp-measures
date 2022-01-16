namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfAcceleration(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfAcceleration>
{
    public static UnitOfAcceleration From(UnitOfVelocity unitOfSpeed, UnitOfTime unitOfTime) => new(unitOfSpeed.Factor / unitOfTime.Factor);

    public static UnitOfAcceleration MetrePerSecondSquared { get; } = From(UnitOfVelocity.MetrePerSecond, UnitOfTime.Second);
    public static UnitOfAcceleration FootPerSecondSquared { get; } = From(UnitOfVelocity.FootPerSecond, UnitOfTime.Second);

    public static UnitOfAcceleration StandardGravity { get; } = MetrePerSecondSquared with { BaseScale = 9.80665 };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfAcceleration(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfAcceleration other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfAcceleration x, UnitOfAcceleration y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfAcceleration x, UnitOfAcceleration y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfAcceleration x, UnitOfAcceleration y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfAcceleration x, UnitOfAcceleration y) => x.Factor >= y.Factor;
}