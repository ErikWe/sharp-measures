namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct AngularVelocity3
{
    /// <summary>Computes average <see cref="AngularVelocity3"/> according to { <paramref name="rotation"/> / <paramref name="time"/> },
    /// where <paramref name="rotation"/> is the change in <see cref="Rotation3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static AngularVelocity3 From(Rotation3 rotation, Time time) => new(rotation.Components / time.Magnitude);

    /// <summary>Computes change in <see cref="Rotation3"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Rotation3 Multiply(Time time) => Rotation3.From(this, time);
    /// <summary>Computes change in <see cref="Rotation3"/> according to { <paramref name="angularVelocity"/> ∙ <paramref name="time"/> }.</summary>
    public static Rotation3 operator *(AngularVelocity3 angularVelocity, Time time) => angularVelocity.Multiply(time);

    /// <summary>Computes <see cref="AngularMomentum3"/> according to { <see langword="this"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public AngularMomentum3 Multiply(MomentOfInertia momentOfInertia) => AngularMomentum3.From(momentOfInertia, this);
    /// <summary>Computes <see cref="AngularMomentum3"/> according to { <paramref name="angularVelocity"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public static AngularMomentum3 operator *(AngularVelocity3 angularVelocity, MomentOfInertia momentOfInertia) => angularVelocity.Multiply(momentOfInertia);

    /// <summary>Computes average <see cref="AngularAcceleration3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public AngularAcceleration3 Divide(Time time) => AngularAcceleration3.From(this, time);
    /// <summary>Computes average <see cref="AngularAcceleration3"/> according to { <paramref name="angularVelocity"/> / <paramref name="time"/> }.</summary>
    public static AngularAcceleration3 operator /(AngularVelocity3 angularVelocity, Time time) => angularVelocity.Divide(time);
}
