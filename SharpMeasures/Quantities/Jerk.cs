namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Jerk
{
    /// <summary>Computes <see cref="Jerk"/> according to { <paramref name="acceleration"/> / <paramref name="time"/> },
    /// where <paramref name="acceleration"/> is the change in <see cref="Acceleration"/> over some duration <paramref name="time"/>.</summary>
    public static Jerk From(Acceleration acceleration, Time time) => new(acceleration.Magnitude / time.Magnitude);
}
