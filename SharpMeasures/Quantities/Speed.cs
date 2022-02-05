namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Speed
{
    /// <summary>Computes average <see cref="Speed"/> according to { <see cref="Speed"/> = <paramref name="distance"/> / <paramref name="time"/> },
    /// where <paramref name="distance"/> is the <see cref="Distance"/> covered over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Speed From(Distance distance, Time time) => new(distance.Magnitude / time.Magnitude);
}
