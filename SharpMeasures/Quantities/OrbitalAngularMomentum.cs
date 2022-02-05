namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularMomentum
{
    /// <summary>Computes <see cref="OrbitalAngularMomentum"/> according to { <see cref="OrbitalAngularMomentum"/>
    /// = <paramref name="momentOfInertia"/> ∗ <paramref name="orbitalAngularSpeed"/> }.</summary>
    public static OrbitalAngularMomentum From(MomentOfInertia momentOfInertia, OrbitalAngularSpeed orbitalAngularSpeed) => new(momentOfInertia.Magnitude * orbitalAngularSpeed.Magnitude);
}
