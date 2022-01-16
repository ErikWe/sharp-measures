namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfMassFlowRate(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfMassFlowRate>
{
    public static UnitOfMassFlowRate From(UnitOfMass unitOfMass, UnitOfTime unitOfTime) => new(unitOfMass.Factor / unitOfTime.Factor);

    public static UnitOfMassFlowRate KilogramPerSecond { get; } = From(UnitOfMass.Kilogram, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfMassFlowRate(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfMassFlowRate other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.Factor >= y.Factor;
}