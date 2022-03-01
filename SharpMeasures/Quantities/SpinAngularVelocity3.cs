namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpinAngularVelocity3
{
    /// <summary>Computes average <see cref="SpinAngularVelocity3"/> according to { <paramref name="rotation"/> / <paramref name="time"/> },
    /// where <paramref name="rotation"/> is the change in <see cref="Rotation3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static SpinAngularVelocity3 From(Rotation3 rotation, Time time) => new(rotation.Components / time.Magnitude);

    /// <summary>Computes <see cref="SpinAngularMomentum3"/> according to { <see langword="this"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public SpinAngularMomentum3 Multiply(MomentOfInertia momentOfInertia) => SpinAngularMomentum3.From(momentOfInertia, this);
    /// <summary>Computes <see cref="SpinAngularMomentum3"/> according to { <paramref name="spinAngularVelocity"/> ∙ <paramref name="momentOfInertia"/> }.</summary>
    public static SpinAngularMomentum3 operator *(SpinAngularVelocity3 spinAngularVelocity, MomentOfInertia momentOfInertia) => spinAngularVelocity.Multiply(momentOfInertia);

    /// <summary>Computes average <see cref="SpinAngularAcceleration3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public SpinAngularAcceleration3 Divide(Time time) => SpinAngularAcceleration3.From(this, time);
    /// <summary>Computes average <see cref="SpinAngularAcceleration3"/> according to { <paramref name="spinAngularVelocity"/> / <paramref name="time"/> }.</summary>
    public static SpinAngularAcceleration3 operator /(SpinAngularVelocity3 spinAngularVelocity, Time time) => spinAngularVelocity.Divide(time);
}
