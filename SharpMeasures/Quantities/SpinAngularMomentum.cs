namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpinAngularMomentum
{
    /// <summary>Computes <see cref="SpinAngularMomentum"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="spinAngularSpeed"/> }.</summary>
    public static OrbitalAngularMomentum From(MomentOfInertia momentOfInertia, SpinAngularSpeed spinAngularSpeed) => new(momentOfInertia.Magnitude * spinAngularSpeed.Magnitude);
}
