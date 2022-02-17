namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Jerk3
{
    /// <summary>Computes <see cref="Jerk3"/> according to { <paramref name="acceleration"/> / <paramref name="time"/> },
    /// where <paramref name="acceleration"/> is the change in <see cref="Acceleration3"/> over some duration <paramref name="time"/>.</summary>
    public static Jerk3 From(Acceleration3 acceleration, Time time) => new(acceleration.Components / time.Magnitude);
}
