namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularMomentum
{
    /// <summary>Computes <see cref="AngularMomentum"/> according to { <see cref="AngularMomentum"/> = <paramref name="momentOfInertia"/> ∗ <paramref name="angularSpeed"/> },
    /// where <paramref name="momentOfInertia"/> is the <see cref="MomentOfInertia"/> of an object spinning with <see cref="AngularSpeed"/> <paramref name="angularSpeed"/>.</summary>
    public static AngularMomentum From(MomentOfInertia momentOfInertia, AngularSpeed angularSpeed) => new(momentOfInertia.Magnitude * angularSpeed.Magnitude);
}
