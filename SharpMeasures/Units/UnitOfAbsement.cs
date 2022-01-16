namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfAbsement(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfAbsement>
{
    public static UnitOfAbsement From(UnitOfLength unitOfLength, UnitOfTime unitOfTime) => new(unitOfLength.Factor * unitOfTime.Factor);

    public static UnitOfAbsement MetreSecond { get; } = From(UnitOfLength.Metre, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfAbsement(double baseScale) : this(baseScale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfAbsement other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfAbsement x, UnitOfAbsement y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfAbsement x, UnitOfAbsement y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfAbsement x, UnitOfAbsement y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfAbsement x, UnitOfAbsement y) => x.Factor >= y.Factor;
}