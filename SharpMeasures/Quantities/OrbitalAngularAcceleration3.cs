namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularAcceleration3
{
    /// <summary>Computes average <see cref="OrbitalAngularAcceleration3"/> according to { <paramref name="orbitalAngularVelocity"/>
    /// / <paramref name="time"/> }, where <paramref name="orbitalAngularVelocity"/> is the change in <see cref="OrbitalAngularVelocity3"/> over some
    /// <see cref="Time"/> <paramref name="time"/>.</summary>
    public static OrbitalAngularAcceleration3 From(OrbitalAngularVelocity3 orbitalAngularVelocity, Time time) => new(orbitalAngularVelocity.Components / time.Magnitude);
}
