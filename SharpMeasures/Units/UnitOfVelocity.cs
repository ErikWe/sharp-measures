namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfVelocity(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfVelocity>
{
    public static UnitOfVelocity From(UnitOfLength unitOfLength, UnitOfTime unitOfTime) => new(unitOfLength.Factor / unitOfTime.Factor);

    public static UnitOfVelocity MetrePerSecond { get; } = From(UnitOfLength.Metre, UnitOfTime.Second);
    public static UnitOfVelocity KilometrePerHour { get; } = From(UnitOfLength.Kilometre, UnitOfTime.Hour);

    public static UnitOfVelocity FootPerSecond { get; } = From(UnitOfLength.Foot, UnitOfTime.Second);
    public static UnitOfVelocity MilePerHour { get; } = From(UnitOfLength.Mile, UnitOfTime.Hour);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfVelocity(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfVelocity other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfVelocity x, UnitOfVelocity y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfVelocity x, UnitOfVelocity y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfVelocity x, UnitOfVelocity y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfVelocity x, UnitOfVelocity y) => x.Factor >= y.Factor;
}