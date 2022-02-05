namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Time
{
    /// <summary>Computes <see cref="Time"/> according to { <see cref="Time"/> = <paramref name="distance"/> / <paramref name="speed"/> },
    /// where <paramref name="speed"/> is the average <see cref="Speed"/> over a <see cref="Distance"/> <paramref name="distance"/>.</summary>
    public static Time From(Distance distance, Speed speed) => new(distance.Magnitude / speed.Magnitude);
}
