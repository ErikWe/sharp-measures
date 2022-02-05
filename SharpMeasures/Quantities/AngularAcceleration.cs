namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularAcceleration
{
    /// <summary>Computes average <see cref="AngularAcceleration"/> according to { <see cref="AngularAcceleration"/> = <paramref name="angularSpeed"/> / <paramref name="time"/> },
    /// where <paramref name="angularSpeed"/> is the change in <see cref="AngularSpeed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static AngularAcceleration From(AngularSpeed angularSpeed, Time time) => new(angularSpeed.Magnitude / time.Magnitude);
}
