namespace ErikWe.SharpMeasures.Quantities;

using System;

public readonly partial record struct Angle
{
    /// <summary>Computes <see cref="Angle"/> according to { <see cref="Angle"/> = <paramref name="angularSpeed"/> ∗ <paramref name="time"/> },
    /// where <paramref name="angularSpeed"/> is the average <see cref="AngularSpeed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Angle From(AngularSpeed angularSpeed, Time time) => new(angularSpeed.Magnitude * time.Magnitude);

    /// <summary>Computes the sine of the <see cref="Angle"/>.</summary>
    public Scalar Sin() => new(Math.Sin(Magnitude));

    /// <summary>Computes the cosine of the <see cref="Angle"/>.</summary>
    public Scalar Cos() => new(Math.Cos(Magnitude));
}
