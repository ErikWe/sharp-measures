namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct TemperatureDifference
{
    /// <summary>Computes <see cref="TemperatureDifference"/> according to { <paramref name="finalTemperature"/> - <paramref name="initialTemperature"/> }.</summary>
    public static TemperatureDifference From(Temperature initialTemperature, Temperature finalTemperature) => new(finalTemperature.Magnitude - initialTemperature.Magnitude);

    /// <summary>Computes final <see cref="Temperature"/> according to { <see langword="this"/> + <paramref name="initialTemperature"/> }.</summary>
    public Temperature Add(Temperature initialTemperature) => Temperature.From(initialTemperature, this);
    /// <summary>Computes final <see cref="Temperature"/> according to { <paramref name="temperatureDifference"/> + <paramref name="initialTemperature"/> }.</summary>
    public static Temperature operator +(TemperatureDifference temperatureDifference, Temperature initialTemperature) => temperatureDifference.Add(initialTemperature);

    /// <summary>Computes <see cref="TemperatureGradient"/> according to { <see langword="this"/> / <paramref name="distance"/> }.</summary>
    public TemperatureGradient Divide(Distance distance) => TemperatureGradient.From(this, distance);
    /// <summary>Computes <see cref="TemperatureGradient"/> according to { <paramref name="temperatureDifference"/> / <paramref name="distance"/> }.</summary>
    public static TemperatureGradient operator /(TemperatureDifference temperatureDifference, Distance distance) => temperatureDifference.Divide(distance);
}
