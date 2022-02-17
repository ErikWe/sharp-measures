namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Impulse3
{
    /// <summary>Computes <see cref="Impulse3"/> according to { <paramref name="force"/> ∙ <paramref name="time"/> },
    /// where <paramref name="force"/> is the average <see cref="Force3"/> applied over some duration <paramref name="time"/>.</summary>
    public static Impulse3 From(Force3 force, Time time) => new(force.Components * time.Magnitude);
}
