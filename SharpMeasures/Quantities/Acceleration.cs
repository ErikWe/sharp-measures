namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Acceleration
{
    /// <summary>Computes average <see cref="Acceleration"/> according to { <see cref="Acceleration"/> = <paramref name="speed"/> / <paramref name="time"/> },
    /// where <paramref name="speed"/> is the change in <see cref="Speed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Acceleration From(Speed speed, Time time) => new(speed.Magnitude / time.Magnitude);
}
