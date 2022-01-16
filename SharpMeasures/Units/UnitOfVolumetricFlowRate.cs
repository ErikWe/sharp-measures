namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfVolumetricFlowRate(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfVolumetricFlowRate>
{
    public static UnitOfVolumetricFlowRate From(UnitOfVolume unitOfVolume, UnitOfTime unitOfTime) => new(unitOfVolume.Factor / unitOfTime.Factor);

    public static UnitOfVolumetricFlowRate CubicMetrePerSecond { get; } = From(UnitOfVolume.CubicMetre, UnitOfTime.Second);
    public static UnitOfVolumetricFlowRate LitrePerSecond { get; } = From(UnitOfVolume.Litre, UnitOfTime.Second);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfVolumetricFlowRate(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfVolumetricFlowRate other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfVolumetricFlowRate x, UnitOfVolumetricFlowRate y) => x.Factor >= y.Factor;
}