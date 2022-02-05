namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct TemperatureGradient
{
    /// <summary>Computes <see cref="TemperatureGradient"/> according to { <see cref="TemperatureGradient"/>
    /// = <paramref name="temperatureDifference"/> / <paramref name="distance"/> }.</summary>
    public static TemperatureGradient From(TemperatureDifference temperatureDifference, Distance distance) => new(temperatureDifference.Magnitude / distance.Magnitude);
}
