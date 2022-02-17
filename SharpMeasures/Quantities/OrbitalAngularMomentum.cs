namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularMomentum
{
    /// <summary>Computes <see cref="OrbitalAngularMomentum"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="orbitalAngularSpeed"/> }.</summary>
    public static OrbitalAngularMomentum From(MomentOfInertia momentOfInertia, OrbitalAngularSpeed orbitalAngularSpeed) => new(momentOfInertia.Magnitude * orbitalAngularSpeed.Magnitude);
}
