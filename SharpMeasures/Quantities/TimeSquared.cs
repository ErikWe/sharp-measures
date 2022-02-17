namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct TimeSquared
{
    /// <summary>Computes <see cref="TimeSquared"/> according to { <paramref name="distance"/> / <paramref name="acceleration"/> },
    /// where <paramref name="acceleration"/> is the average <see cref="Acceleration"/> over a <see cref="Distance"/> <paramref name="distance"/>.</summary>
    public static TimeSquared From(Distance distance, Acceleration acceleration) => new(distance.Magnitude / acceleration.Magnitude);
}
