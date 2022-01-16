namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfAngularAcceleration(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfAngularAcceleration>
{
    public static UnitOfAngularAcceleration From(UnitOfAngularVelocity unitOfAngularVelocity, UnitOfTime unitOfTime) => new(unitOfAngularVelocity.Factor / unitOfTime.Factor);

    public static UnitOfAngularAcceleration RadianPerSecondSquared { get; } = From(UnitOfAngularVelocity.RadianPerSecond, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfAngularAcceleration(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfAngularAcceleration other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfAngularAcceleration x, UnitOfAngularAcceleration y) => x.Factor >= y.Factor;
}