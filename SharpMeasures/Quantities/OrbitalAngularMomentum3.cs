namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularMomentum3
{
    /// <summary>Computes <see cref="OrbitalAngularMomentum3"/> according to { <paramref name="momentOfInertia"/> ∙ <paramref name="orbitalAngularVelocity"/> }.</summary>
    public static OrbitalAngularMomentum3 From(MomentOfInertia momentOfInertia, OrbitalAngularVelocity3 orbitalAngularVelocity)
        => new(momentOfInertia.Magnitude * orbitalAngularVelocity.Components);
}
