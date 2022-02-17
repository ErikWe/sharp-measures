namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Momentum3
{
    /// <summary>Computes <see cref="Momentum3"/> according to { <paramref name="mass"/> ∙ <paramref name="velocity"/> }.</summary>
    public static Momentum3 From(Mass mass, Velocity3 velocity) => new(mass.Magnitude * velocity.Components);
}
