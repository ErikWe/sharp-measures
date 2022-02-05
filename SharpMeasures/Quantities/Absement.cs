namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Absement
{
    /// <summary>Computes <see cref="Absement"/> according to { <see cref="Absement"/> = <paramref name="distance"/> ∗ <paramref name="time"/> },
    /// where <paramref name="distance"/> is the average displacement over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Absement From(Distance distance, Time time) => new(distance.Magnitude * time.Magnitude);
}
