namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfSpatialFrequency(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfSpatialFrequency>
{
    public static UnitOfSpatialFrequency From(UnitOfLength unitOfLength) => new(1 / unitOfLength.Factor);

    public static UnitOfSpatialFrequency PerMetre { get; } = From(UnitOfLength.Metre);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfSpatialFrequency(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfSpatialFrequency other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfSpatialFrequency x, UnitOfSpatialFrequency y) => x.Factor >= y.Factor;
}