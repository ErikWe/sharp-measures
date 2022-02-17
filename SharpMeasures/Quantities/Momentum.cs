namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Momentum
{
    /// <summary>Computes <see cref="Momentum"/> according to { <paramref name="mass"/> ∙ <paramref name="speed"/> }.</summary>
    public static Momentum From(Mass mass, Speed speed) => new(mass.Magnitude * speed.Magnitude);
}
