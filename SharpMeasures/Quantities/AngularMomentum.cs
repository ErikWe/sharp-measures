namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularMomentum
{
    /// <summary>Computes <see cref="AngularMomentum"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="angularSpeed"/> },
    /// where <paramref name="momentOfInertia"/> is the <see cref="MomentOfInertia"/> of an object spinning with <see cref="AngularSpeed"/> <paramref name="angularSpeed"/>.</summary>
    public static AngularMomentum From(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed) => new(momentOfInertia.Magnitude * angularSpeed.Magnitude);

    /// <summary>Computes { <see langword="this"/> ∙ <paramref name="angularSpeed"/> }, as a <see cref="RotationalKineticEnergy"/>.</summary>
    /// <remarks>This is just the product of the magnitudes, use <see cref="RotationalKineticEnergy.From(AngularMomentum, AngularSpeed)"/> to
    /// compute the actual <see cref="RotationalKineticEnergy"/>.</remarks>
    public RotationalKineticEnergy Multiply(AngularSpeed angularSpeed) => new(Magnitude * angularSpeed.Magnitude);
    /// <summary>Computes { <paramref name="angularMomentum"/> ∙ <paramref name="angularSpeed"/> }, as a <see cref="RotationalKineticEnergy"/>.</summary>
    /// <remarks>This is just the product of the magnitudes, use <see cref="RotationalKineticEnergy.From(AngularMomentum, AngularSpeed)"/> to
    /// compute the actual <see cref="RotationalKineticEnergy"/>.</remarks>
    public static RotationalKineticEnergy operator *(AngularMomentum angularMomentum, AngularSpeed angularSpeed) => angularMomentum.Multiply(angularSpeed);

    /// <summary>Computes <see cref="MomentOfInertia"/> according to { <see langword="this"/> / <paramref name="angularSpeed"/> }.</summary>
    public MomentOfInertia Divide(AngularSpeed angularSpeed) => MomentOfInertia.From(this, angularSpeed);
    /// <summary>Computes <see cref="MomentOfInertia"/> according to { <paramref name="angularMomentum"/> / <paramref name="angularSpeed"/> }.</summary>
    public static MomentOfInertia operator /(AngularMomentum angularMomentum, AngularSpeed angularSpeed) => angularMomentum.Divide(angularSpeed);

    /// <summary>Computes <see cref="SpecificAngularMomentum"/> according to { <see langword="this"/> / <paramref name="mass"/> }.</summary>
    public SpecificAngularMomentum Divide(Mass mass) => SpecificAngularMomentum.From(this, mass);
    /// <summary>Computes <see cref="SpecificAngularMomentum"/> according to { <paramref name="angularMomentum"/> / <paramref name="mass"/> }.</summary>
    public static SpecificAngularMomentum operator /(AngularMomentum angularMomentum, Mass mass) => angularMomentum.Divide(mass);
}
