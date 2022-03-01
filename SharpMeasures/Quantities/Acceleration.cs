namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Acceleration
{
    /// <summary>Computes average <see cref="Acceleration"/> according to { <paramref name="speed"/> / <paramref name="time"/> },
    /// where <paramref name="speed"/> is the change in <see cref="Speed"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Acceleration From(Speed speed, Time time) => new(speed.Magnitude / time.Magnitude);

    /// <summary>Computes change in <see cref="Speed"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Speed Multiply(Time time) => Speed.From(this, time);
    /// <summary>Computes change in <see cref="Speed"/> according to { <paramref name="acceleration"/> ∙ <paramref name="time"/> }.</summary>
    public static Speed operator *(Acceleration acceleration, Time time) => acceleration.Multiply(time);

    /// <summary>Computes <see cref="Force"/> according to { <see langword="this"/> ∙ <paramref name="mass"/> }.</summary>
    public Force Multiply(Mass mass) => Force.From(mass, this);
    /// <summary>Computes <see cref="Force"/> according to { <paramref name="acceleration"/> ∙ <paramref name="mass"/> }.</summary>
    public static Force operator *(Acceleration acceleration, Mass mass) => acceleration.Multiply(mass);

    /// <summary>Computes <see cref="Jerk"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Jerk Divide(Time time) => Jerk.From(this, time);
    /// <summary>Computes <see cref="Jerk"/> according to { <paramref name="acceleration"/> / <paramref name="time"/> }.</summary>
    public static Jerk operator /(Acceleration acceleration, Time time) => acceleration.Divide(time);
}
