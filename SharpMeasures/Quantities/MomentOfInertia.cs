namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct MomentOfInertia
{
    /// <summary>Computes <see cref="MomentOfInertia"/> according to { <paramref name="angularMomentum"/> / <paramref name="angularSpeed"/> }.</summary>
    public static MomentOfInertia From(AngularMomentum angularMomentum, AngularSpeed angularSpeed) => new(angularMomentum.Magnitude / angularSpeed.Magnitude);

    /// <summary>Computes <see cref="MomentOfInertia"/> of a point object according to { <paramref name="mass"/> ∙ <paramref name="distance"/>² },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object at <see cref="Distance"/> <paramref name="distance"/> from the pivot.</summary>
    public static MomentOfInertia From(Mass mass, Distance distance) => new(mass.Magnitude * Math.Pow(distance.Magnitude, 2));
}
