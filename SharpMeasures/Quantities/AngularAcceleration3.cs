namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularAcceleration3
{
    /// <summary>Computes average <see cref="AngularAcceleration3"/> according to { <paramref name="angularVelocity"/> / <paramref name="time"/> },
    /// where <paramref name="angularVelocity"/> is the change in <see cref="AngularVelocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static AngularAcceleration3 From(AngularVelocity3 angularVelocity, Time time) => new(angularVelocity.Components / time.Magnitude);
}
