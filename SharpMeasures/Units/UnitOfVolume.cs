namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfVolume(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfVolume>
{
    public static UnitOfVolume From(UnitOfLength unitOfLength) => new(Math.Pow(unitOfLength.Factor, 3));

    public static UnitOfVolume CubicDecimetre { get; } = From(UnitOfLength.Decimetre);
    public static UnitOfVolume CubicMetre { get; } = From(UnitOfLength.Metre);

    public static UnitOfVolume Litre { get; } = CubicDecimetre;
    public static UnitOfVolume Millilitre { get; } = Litre with { Prefix = MetricPrefix.Milli };
    public static UnitOfVolume Centilitre { get; } = Litre with { Prefix = MetricPrefix.Centi };
    public static UnitOfVolume Decilitre { get; } = Litre with { Prefix = MetricPrefix.Deci };

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfVolume(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfVolume other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfVolume x, UnitOfVolume y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfVolume x, UnitOfVolume y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfVolume x, UnitOfVolume y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfVolume x, UnitOfVolume y) => x.Factor >= y.Factor;
}