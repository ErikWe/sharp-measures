namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Momentum
{
    /// <summary>Computes <see cref="Momentum"/> according to { <paramref name="mass"/> ∙ <paramref name="speed"/> }.</summary>
    public static Momentum From(Mass mass, Speed speed) => new(mass.Magnitude * speed.Magnitude);

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="speed"/> }, as a <see cref="TranslationalKineticEnergy"/>.</summary>
    /// <remarks>This is just the product of the magnitudes, use <see cref="TranslationalKineticEnergy.From(Momentum, Speed)"/> to
    /// compute the actual <see cref="TranslationalKineticEnergy"/>.</remarks>
    public TranslationalKineticEnergy Multiply(Speed speed) => new(Magnitude * speed.Magnitude);
    /// <summary>Computes { <paramref name="momentum"/> ∙ <paramref name="speed"/> }, as a <see cref="TranslationalKineticEnergy"/>.</summary>
    /// <remarks>This is just the product of the magnitudes, use <see cref="TranslationalKineticEnergy.From(Momentum, Speed)"/> to
    /// compute the actual <see cref="TranslationalKineticEnergy"/>.</remarks>
    public static TranslationalKineticEnergy operator *(Momentum momentum, Speed speed) => momentum.Multiply(speed);
}
