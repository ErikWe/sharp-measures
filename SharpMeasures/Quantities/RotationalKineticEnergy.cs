namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct RotationalKineticEnergy
{
    /// <summary>Computes <see cref="RotationalKineticEnergy"/> according to { 1/2 ∙ <paramref name="momentOfInertia"/> ∙ <paramref name="angularSpeed"/>² }.</summary>
    public static RotationalKineticEnergy From(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed)
        => From(AngularMomentum.From(momentOfInertia, angularSpeed), angularSpeed);

    /// <summary>Computes <see cref="RotationalKineticEnergy"/> according to { 1/2 ∙ <paramref name="angularMomentum"/> ∙ <paramref name="angularSpeed"/> }.</summary>
    public static RotationalKineticEnergy From(AngularMomentum angularMomentum, AngularSpeed angularSpeed) => new(0.5 * angularMomentum.Magnitude * angularSpeed.Magnitude);

    /// <summary>Computes the total <see cref="KineticEnergy"/> of an object, according to { <see langword="this"/> + <paramref name="translationalKineticEnergy"/> }.</summary>
    public KineticEnergy Add(TranslationalKineticEnergy translationalKineticEnergy) => KineticEnergy.From(translationalKineticEnergy, this);
    /// <summary>Computes the total <see cref="KineticEnergy"/> of an object, according to { <paramref name="rotationalKineticEnergy"/>
    /// + <paramref name="translationalKineticEnergy"/> }.</summary>
    public static KineticEnergy operator +(RotationalKineticEnergy rotationalKineticEnergy, TranslationalKineticEnergy translationalKineticEnergy)
        => rotationalKineticEnergy.Add(translationalKineticEnergy);
}
