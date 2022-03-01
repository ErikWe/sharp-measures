namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct TranslationalKineticEnergy
{
    /// <summary>Computes <see cref="TranslationalKineticEnergy"/> according to { 1/2 ∙ <paramref name="mass"/> ∙ <paramref name="speed"/>² }.</summary>
    public static TranslationalKineticEnergy From(Mass mass, Speed speed) => From(Momentum.From(mass, speed), speed);

    /// <summary>Computes <see cref="TranslationalKineticEnergy"/> according to { 1/2 ∙ <paramref name="momentum"/> ∙ <paramref name="speed"/> }.</summary>
    public static TranslationalKineticEnergy From(Momentum momentum, Speed speed) => new(0.5 * momentum.Magnitude * speed.Magnitude);

    /// <summary>Computes the total <see cref="KineticEnergy"/> of an object, according to { <see langword="this"/> + <paramref name="rotationalKineticEnergy"/> }.</summary>
    public KineticEnergy Add(RotationalKineticEnergy rotationalKineticEnergy) => KineticEnergy.From(this, rotationalKineticEnergy);
    /// <summary>Computes the total <see cref="KineticEnergy"/> of an object, according to { <paramref name="translationalKineticEnergy"/>
    /// + <paramref name="rotationalKineticEnergy"/> }.</summary>
    public static KineticEnergy operator +(TranslationalKineticEnergy translationalKineticEnergy, RotationalKineticEnergy rotationalKineticEnergy)
        => translationalKineticEnergy.Add(rotationalKineticEnergy);
}
