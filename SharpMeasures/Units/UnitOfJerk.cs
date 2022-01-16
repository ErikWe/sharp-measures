namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfJerk(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfJerk>
{
    public static UnitOfJerk From(UnitOfAcceleration unitOfAcceleration, UnitOfTime unitOfTime) => new(unitOfAcceleration.Factor / unitOfTime.Factor);

    public static UnitOfJerk MetrePerSecondCubed { get; } = From(UnitOfAcceleration.MetrePerSecondSquared, UnitOfTime.Second);
    public static UnitOfJerk FootPerSecondCubed { get; } = From(UnitOfAcceleration.FootPerSecondSquared, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfJerk(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfJerk other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfJerk x, UnitOfJerk y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfJerk x, UnitOfJerk y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfJerk x, UnitOfJerk y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfJerk x, UnitOfJerk y) => x.Factor >= y.Factor;
}