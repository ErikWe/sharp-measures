namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Temperature
{
    /// <summary>Computes final <see cref="Temperature"/> according to { <see cref="Temperature"/>
    /// = <paramref name="initialTemperature"/> + <paramref name="temperatureDifference"/> }.</summary>
    public static Temperature From(Temperature initialTemperature, TemperatureDifference temperatureDifference) => new(initialTemperature.Magnitude + temperatureDifference.Magnitude);
}
