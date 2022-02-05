namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularSpeed
{
    /// <summary>Computes average <see cref="AngularSpeed"/> according to { <see cref="AngularSpeed"/> = <paramref name="angle"/> / <paramref name="time"/> },
    /// where <paramref name="angle"/> is the <see cref="Angle"/> covered over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static AngularSpeed From(Angle angle, Time time) => new(angle.Magnitude / time.Magnitude);
}
