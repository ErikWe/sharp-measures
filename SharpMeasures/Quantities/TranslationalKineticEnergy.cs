namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct TranslationalKineticEnergy
{
    /// <summary>Computes <see cref="TranslationalKineticEnergy"/> according to { 1/2 ∙ <paramref name="mass"/> ∙ <paramref name="speed"/>² }.</summary>
    public static TranslationalKineticEnergy From(Mass mass, Speed speed) => new(.5 * mass.Magnitude / Math.Pow(speed.Magnitude, 2));
}
