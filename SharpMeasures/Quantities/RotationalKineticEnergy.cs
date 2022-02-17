namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct RotationalKineticEnergy
{
    /// <summary>Computes <see cref="RotationalKineticEnergy"/> according to { 1/2 ∙ <paramref name="momentOfInertia"/> ∙ <paramref name="angularSpeed"/>² }.</summary>
    public static RotationalKineticEnergy From(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed)
        => new(.5 * momentOfInertia.Magnitude / Math.Pow(angularSpeed.Magnitude, 2));
}
