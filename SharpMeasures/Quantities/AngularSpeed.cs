namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularSpeed
{
    /// <summary>Computes average <see cref="AngularSpeed"/> according to { <paramref name="angle"/> / <paramref name="time"/> },
    /// where <paramref name="angle"/> is the <see cref="Angle"/> covered over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static AngularSpeed From(Angle angle, Time time) => new(angle.Magnitude / time.Magnitude);

    /// <summary>Computes change in <see cref="Angle"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Angle Multiply(Time time) => Angle.From(this, time);
    /// <summary>Computes change in <see cref="Angle"/> according to { <paramref name="angularSpeed"/> ∙ <paramref name="time"/> }.</summary>
    public static Angle operator *(AngularSpeed angularSpeed, Time time) => angularSpeed.Multiply(time);

    /// <summary>Computes <see cref="AngularMomentum"/> according to { <see langword="this"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public AngularMomentum Multiply(MomentOfInertia momentOfInertia) => AngularMomentum.From(momentOfInertia, this);
    /// <summary>Computes <see cref="AngularMomentum"/> according to { <paramref name="angularSpeed"/> ∙ <paramref name="momentOfInertia"/>  }.</summary>
    public static AngularMomentum operator *(AngularSpeed angularSpeed, MomentOfInertia momentOfInertia) => angularSpeed.Multiply(momentOfInertia);

    /// <summary>Computes average <see cref="AngularAcceleration"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public AngularAcceleration Divide(Time time) => AngularAcceleration.From(this, time);
    /// <summary>Computes average <see cref="AngularAcceleration"/> according to { <paramref name="angularSpeed"/> / <paramref name="time"/> }.</summary>
    public static AngularAcceleration operator /(AngularSpeed angularSpeed, Time time) => angularSpeed.Divide(time);
}
