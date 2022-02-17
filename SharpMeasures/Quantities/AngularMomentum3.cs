namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularMomentum3
{
    /// <summary>Computes <see cref="AngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="angularVelocity"/> },
    /// where <paramref name="momentOfInertia"/> is the <see cref="MomentOfInertia"/> of an object spinning with <see cref="AngularVelocity3"/> <paramref name="angularVelocity"/>.</summary>
    public static AngularMomentum3 From(MomentOfInertia momentOfInertia, AngularVelocity3 angularVelocity) => new(momentOfInertia.Magnitude * angularVelocity.Components);
}
