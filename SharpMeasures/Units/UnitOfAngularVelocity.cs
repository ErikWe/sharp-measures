namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfAngularVelocity(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfAngularVelocity>
{
    public static UnitOfAngularVelocity From(UnitOfAngle unitOfAngle, UnitOfTime unitOfTime) => new(unitOfAngle.Factor / unitOfTime.Factor);

    public static UnitOfAngularVelocity RadianPerSecond { get; } = From(UnitOfAngle.Radian, UnitOfTime.Second);
    public static UnitOfAngularVelocity DegreePerSecond { get; } = From(UnitOfAngle.Degree, UnitOfTime.Second);
    public static UnitOfAngularVelocity TurnPerSecond { get; } = From(UnitOfAngle.Turn, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfAngularVelocity(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfAngularVelocity other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfAngularVelocity x, UnitOfAngularVelocity y) => x.Factor >= y.Factor;
}