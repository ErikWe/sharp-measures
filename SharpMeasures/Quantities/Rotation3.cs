namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Rotation3
{
    /// <summary>Computes <see cref="Rotation3"/> according to { <paramref name="angularVelocity"/> ∙ <paramref name="time"/> },
    /// where <paramref name="angularVelocity"/> is the average <see cref="AngularVelocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Rotation3 From(AngularVelocity3 angularVelocity, Time time) => new(angularVelocity.Components * time.Magnitude);

    /// <summary>Computes average <see cref="AngularVelocity3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public AngularVelocity3 Divide(Time time) => AngularVelocity3.From(this, time);
    /// <summary>Computes average <see cref="AngularVelocity3"/> according to { <paramref name="rotation"/> / <paramref name="time"/> }.</summary>
    public static AngularVelocity3 operator /(Rotation3 rotation, Time time) => rotation.Divide(time);
}
