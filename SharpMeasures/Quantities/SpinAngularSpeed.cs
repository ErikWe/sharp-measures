namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpinAngularSpeed
{
    /// <summary>Computes average <see cref="SpinAngularSpeed"/> according to { <paramref name="angle"/> / <paramref name="time"/> }, where <paramref name="angle"/> is the change in
    /// <see cref="Angle"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static SpinAngularSpeed From(Angle angle, Time time) => new(angle.Magnitude / time.Magnitude);

    /// <summary>Computes <see cref="SpinAngularMomentum"/> according to { <see langword="this"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public SpinAngularMomentum Multiply(MomentOfInertia momentOfInertia) => SpinAngularMomentum.From(momentOfInertia, this);
    /// <summary>Computes <see cref="SpinAngularMomentum"/> according to { <paramref name="spinAngularSpeed"/> ∙ <paramref name="momentOfInertia"/>  }.</summary>
    public static SpinAngularMomentum operator *(SpinAngularSpeed spinAngularSpeed, MomentOfInertia momentOfInertia) => spinAngularSpeed.Multiply(momentOfInertia);

    /// <summary>Computes average <see cref="SpinAngularAcceleration"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public SpinAngularAcceleration Divide(Time time) => SpinAngularAcceleration.From(this, time);
    /// <summary>Computes average <see cref="SpinAngularAcceleration"/> according to { <paramref name="spinAngularSpeed"/> / <paramref name="time"/> }.</summary>
    public static SpinAngularAcceleration operator /(SpinAngularSpeed spinAngularSpeed, Time time) => spinAngularSpeed.Divide(time);
}
