namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfTorque(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfTorque>
{
    public static UnitOfTorque From(UnitOfForce unitOfForce, UnitOfLength unitOfLength) => new(unitOfForce.Factor * unitOfLength.Factor);

    public static UnitOfTorque NewtonMetre { get; } = From(UnitOfForce.Newton, UnitOfLength.Metre);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfTorque(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfTorque other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfTorque x, UnitOfTorque y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfTorque x, UnitOfTorque y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfTorque x, UnitOfTorque y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfTorque x, UnitOfTorque y) => x.Factor >= y.Factor;
}