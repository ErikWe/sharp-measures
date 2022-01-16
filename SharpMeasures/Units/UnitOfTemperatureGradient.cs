namespace ErikWe.SharpMeasures.Units;

using System;

public readonly record struct UnitOfTemperatureGradient(double BaseScale, MetricPrefix Prefix) :
    IComparable<UnitOfTemperatureGradient>
{
    public static UnitOfTemperatureGradient From(UnitOfTemperatureDifference unitOfTemperatureDifference, UnitOfLength unitOfLength)
        => new(unitOfTemperatureDifference.Factor / unitOfLength.Factor);

    public static UnitOfTemperatureGradient KelvinPerMetre { get; } = From(UnitOfTemperatureDifference.Kelvin, UnitOfLength.Metre);
    public static UnitOfTemperatureGradient CelsiusPerMetre { get; } = KelvinPerMetre;

    public static UnitOfTemperatureGradient RankinePerMetre { get; } = From(UnitOfTemperatureDifference.Rankine, UnitOfLength.Metre);
    public static UnitOfTemperatureGradient FahrenheitPerMetre { get; } = RankinePerMetre;

    public double Factor => BaseScale * Prefix.Scale;

    public UnitOfTemperatureGradient(double scale) : this(scale, MetricPrefix.Identity) { }

    public int CompareTo(UnitOfTemperatureGradient other) => Factor.CompareTo(other.Factor);
    public override string ToString() => $"{Factor}";

    public static bool operator <(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.Factor < y.Factor;
    public static bool operator >(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.Factor > y.Factor;
    public static bool operator <=(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.Factor <= y.Factor;
    public static bool operator >=(UnitOfTemperatureGradient x, UnitOfTemperatureGradient y) => x.Factor >= y.Factor;
}