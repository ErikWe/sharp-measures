namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpinAngularAcceleration
{
    /// <summary>Computes average <see cref="SpinAngularAcceleration"/> according to { <paramref name="spinAngularSpeed"/> / <paramref name="time"/> }, where <paramref name="spinAngularSpeed"/> is the change in
    /// <see cref="SpinAngularSpeed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static SpinAngularAcceleration From(SpinAngularSpeed spinAngularSpeed, Time time) => new(spinAngularSpeed.Magnitude / time.Magnitude);
}
