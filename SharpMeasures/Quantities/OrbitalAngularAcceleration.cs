namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularAcceleration
{
    /// <summary>Computes average <see cref="OrbitalAngularAcceleration"/> according to { <see cref="OrbitalAngularAcceleration"/>
    /// = <paramref name="orbitalAngularSpeed"/> / <paramref name="time"/> }, where <paramref name="orbitalAngularSpeed"/> is the change in
    /// <see cref="OrbitalAngularSpeed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static OrbitalAngularAcceleration From(OrbitalAngularSpeed orbitalAngularSpeed, Time time) => new(orbitalAngularSpeed.Magnitude / time.Magnitude);
}
