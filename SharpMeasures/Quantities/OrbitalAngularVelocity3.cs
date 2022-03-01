namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularVelocity3
{
    /// <summary>Computes average <see cref="OrbitalAngularVelocity3"/> according to { <paramref name="rotation"/> / <paramref name="time"/> },
    /// where <paramref name="rotation"/> is the change in <see cref="Rotation3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static OrbitalAngularVelocity3 From(Rotation3 rotation, Time time) => new(rotation.Components / time.Magnitude);

    /// <summary>Computes <see cref="OrbitalAngularMomentum3"/> according to { <see langword="this"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public OrbitalAngularMomentum3 Multiply(MomentOfInertia momentOfInertia) => OrbitalAngularMomentum3.From(momentOfInertia, this);
    /// <summary>Computes <see cref="OrbitalAngularMomentum3"/> according to { <paramref name="orbitalAngularVelocity"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public static OrbitalAngularMomentum3 operator *(OrbitalAngularVelocity3 orbitalAngularVelocity, MomentOfInertia momentOfInertia)
        => orbitalAngularVelocity.Multiply(momentOfInertia);

    /// <summary>Computes average <see cref="OrbitalAngularAcceleration3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public OrbitalAngularAcceleration3 Divide(Time time) => OrbitalAngularAcceleration3.From(this, time);
    /// <summary>Computes average <see cref="OrbitalAngularAcceleration3"/> according to { <paramref name="orbitalAngularVelocity"/> / <paramref name="time"/> }.</summary>
    public static OrbitalAngularAcceleration3 operator /(OrbitalAngularVelocity3 orbitalAngularVelocity, Time time) => orbitalAngularVelocity.Divide(time);
}
