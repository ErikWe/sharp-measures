namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct TemperatureDifference
{
    /// <summary>Computes <see cref="TemperatureDifference"/> according to { <see cref="TemperatureDifference"/>
    /// = <paramref name="initialTemperature"/> - <paramref name="finalTemperature"/> }.</summary>
    public static TemperatureDifference From(Temperature initialTemperature, Temperature finalTemperature) => new(initialTemperature.Magnitude - finalTemperature.Magnitude);
}
