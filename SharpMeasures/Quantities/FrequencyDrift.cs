namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct FrequencyDrift
{
    /// <summary>Computes average <see cref="FrequencyDrift"/> according to { <paramref name="frequency"/> / <paramref name="time"/> },
    /// where <paramref name="frequency"/> is the change in <see cref="Frequency"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static FrequencyDrift From(Frequency frequency, Time time) => new(frequency.Magnitude / time.Magnitude);
}
