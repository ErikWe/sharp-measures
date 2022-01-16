namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfTemperatureDifference(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfTemperatureDifference>
{
    public static UnitOfTemperatureDifference From(UnitOfTemperature unitOfTemperature) => new(unitOfTemperature.BaseScale);

    public static UnitOfTemperatureDifference Kelvin { get; } = From(UnitOfTemperature.Kelvin);
    public static UnitOfTemperatureDifference Celsius { get; } = From(UnitOfTemperature.Celsius);

    public static UnitOfTemperatureDifference Rankine { get; } = From(UnitOfTemperature.Rankine);
    public static UnitOfTemperatureDifference Fahrenheit { get; } = From(UnitOfTemperature.Fahrenheit);

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfTemperatureDifference(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfTemperatureDifference other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfTemperatureDifference x, UnitOfTemperatureDifference y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfTemperatureDifference x, UnitOfTemperatureDifference y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfTemperatureDifference x, UnitOfTemperatureDifference y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfTemperatureDifference x, UnitOfTemperatureDifference y) => x.Factor >= y.Factor;
}