namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularVelocity3
{
    /// <summary>Computes average <see cref="AngularVelocity3"/> according to { <paramref name="rotation"/> / <paramref name="time"/> },
    /// where <paramref name="rotation"/> is the change in <see cref="Rotation3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static AngularVelocity3 From(Rotation3 rotation, Time time) => new(rotation.Components / time.Magnitude);
}
