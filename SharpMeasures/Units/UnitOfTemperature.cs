namespace ErikWe.SharpMeasures.Units;

public readonly record struct UnitOfTemperature(double BaseScale, MetricPrefix Prefix, double Bias)
{
    public static UnitOfTemperature Kelvin { get; } = new() { BaseScale = 1, Bias = 0 };
    public static UnitOfTemperature Celsius { get; } = Kelvin with { Bias = 273.15 };

    public static UnitOfTemperature Rankine { get; } = Kelvin with { BaseScale = 5d / 9 };
    public static UnitOfTemperature Fahrenheit { get; } = Rankine with { Bias = 459.67 };

    public UnitOfTemperature(double bias, double scale) : this(bias, MetricPrefix.Identity, scale) { }

    public override string ToString() => $"{Prefix} x {BaseScale} (+ {Bias})";
}