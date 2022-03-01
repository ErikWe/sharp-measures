namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Frequency
{
    /// <summary>Computes average <see cref="FrequencyDrift"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public FrequencyDrift Divide(Time time) => FrequencyDrift.From(this, time);
    /// <summary>Computes average <see cref="FrequencyDrift"/> according to { <paramref name="frequency"/> / <paramref name="time"/> }.</summary>
    public static FrequencyDrift operator /(Frequency frequency, Time time) => frequency.Divide(time);
}
