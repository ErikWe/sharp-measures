namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpinAngularAcceleration3
{
    /// <summary>Computes average <see cref="SpinAngularAcceleration3"/> according to { <paramref name="spinAngularVelocity"/> / <paramref name="time"/> }, where <paramref name="spinAngularVelocity"/> is the change in
    /// <see cref="SpinAngularVelocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static SpinAngularAcceleration3 From(SpinAngularVelocity3 spinAngularVelocity, Time time) => new(spinAngularVelocity.Components / time.Magnitude);
}
