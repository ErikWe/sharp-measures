namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Acceleration3
{
    /// <summary>Computes average <see cref="Acceleration3"/> according to { <paramref name="velocity"/> / <paramref name="time"/> },
    /// where <paramref name="velocity"/> is the change in <see cref="Velocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Acceleration3 From(Velocity3 velocity, Time time) => new(velocity.Components / time.Magnitude);
}
