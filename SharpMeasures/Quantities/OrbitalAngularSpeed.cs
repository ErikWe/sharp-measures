namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct OrbitalAngularSpeed
{
    /// <summary>Computes average <see cref="OrbitalAngularSpeed"/> according to { <paramref name="angle"/> / <paramref name="time"/> }, where <paramref name="angle"/> is the change in
    /// <see cref="Angle"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static OrbitalAngularSpeed From(Angle angle, Time time) => new(angle.Magnitude / time.Magnitude);
}
