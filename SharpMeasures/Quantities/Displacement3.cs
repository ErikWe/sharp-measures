namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Displacement3
{
    /// <summary>Computes <see cref="Displacement3"/> according to { <paramref name="velocity"/> ∙ <paramref name="time"/> },
    /// where <paramref name="velocity"/> is the average <see cref="Velocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Displacement3 From(Velocity3 velocity, Time time) => new(velocity.Components * time.Magnitude);
}
