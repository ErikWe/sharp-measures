namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularMomentum3
{
    /// <summary>Computes <see cref="AngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="angularVelocity"/> },
    /// where <paramref name="momentOfInertia"/> is the <see cref="MomentOfInertia"/> of an object spinning with <see cref="AngularVelocity3"/>
    /// <paramref name="angularVelocity"/>.</summary>
    public static AngularMomentum3 From(MomentOfInertia momentOfInertia, AngularVelocity3 angularVelocity) => new(momentOfInertia.Magnitude * angularVelocity.Components);

    /// <summary>Computes <see cref="SpecificAngularMomentum3"/> according to { <see langword="this"/> / <paramref name="mass"/> }.</summary>
    public SpecificAngularMomentum3 Divide(Mass mass) => SpecificAngularMomentum3.From(this, mass);
    /// <summary>Computes <see cref="SpecificAngularMomentum3"/> according to { <paramref name="angularMomentum"/> / <paramref name="mass"/> }.</summary>
    public static SpecificAngularMomentum3 operator /(AngularMomentum3 angularMomentum, Mass mass) => angularMomentum.Divide(mass);
}
