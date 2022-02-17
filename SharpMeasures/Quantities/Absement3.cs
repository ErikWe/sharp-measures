namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Absement3
{
    /// <summary>Computes <see cref="Absement3"/> according to { <paramref name="displacement"/> ∙ <paramref name="time"/> },
    /// where <paramref name="displacement"/> is the average <see cref="Displacement3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Absement3 From(Displacement3 displacement, Time time) => new(displacement.Components * time.Magnitude);
}
