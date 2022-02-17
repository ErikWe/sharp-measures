namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Rotation3
{
    /// <summary>Computes <see cref="Rotation3"/> according to { <paramref name="angularVelocity"/> ∙ <paramref name="time"/> },
    /// where <paramref name="angularVelocity"/> is the average <see cref="AngularVelocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Rotation3 From(AngularVelocity3 angularVelocity, Time time) => new(angularVelocity.Components * time.Magnitude);
}
