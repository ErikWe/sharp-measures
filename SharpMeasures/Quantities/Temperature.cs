namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Temperature
{
    /// <summary>Computes final <see cref="Temperature"/> according to { <paramref name="initialTemperature"/> + <paramref name="temperatureDifference"/> }.</summary>
    public static Temperature From(Temperature initialTemperature, TemperatureDifference temperatureDifference)
        => new(initialTemperature.Magnitude + temperatureDifference.Magnitude);

    /// <summary>Computes final <see cref="Temperature"/> according to { <see langword="this"/> + <paramref name="temperatureDifference"/> }.</summary>
    public Temperature Add(TemperatureDifference temperatureDifference) => From(this, temperatureDifference);
    /// <summary>Computes final <see cref="Temperature"/> according to { <paramref name="initialTemperature"/> + <paramref name="temperatureDifference"/> }.</summary>
    public static Temperature operator +(Temperature initialTemperature, TemperatureDifference temperatureDifference) => initialTemperature.Add(temperatureDifference);

    /// <summary>Computes <see cref="TemperatureDifference"/> according to { <see langword="this"/> - <paramref name="initialTemperature"/> }.</summary>
    public TemperatureDifference Subtract(Temperature initialTemperature) => TemperatureDifference.From(initialTemperature, this);
    /// <summary>Computes <see cref="TemperatureDifference"/> according to { <paramref name="finalTemperature"/> - <paramref name="initialTemperature"/> }.</summary>
    public static TemperatureDifference operator -(Temperature finalTemperature, Temperature initialTemperature) => finalTemperature.Subtract(initialTemperature);
}
