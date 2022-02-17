namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Impulse
{
    /// <summary>Computes <see cref="Impulse"/> according to { <paramref name="force"/> ∙ <paramref name="time"/> },
    /// where <paramref name="force"/> is the average <see cref="Force"/> applied over some duration <paramref name="time"/>.</summary>
    public static Impulse From(Force force, Time time) => new(force.Magnitude * time.Magnitude);
}
