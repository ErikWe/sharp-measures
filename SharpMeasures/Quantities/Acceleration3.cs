namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Acceleration3
{
    /// <summary>Computes average <see cref="Acceleration3"/> according to { <paramref name="velocity"/> / <paramref name="time"/> },
    /// where <paramref name="velocity"/> is the change in <see cref="Velocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Acceleration3 From(Velocity3 velocity, Time time) => new(velocity.Components / time.Magnitude);

    /// <summary>Computes <see cref="Force3"/> according to { <see langword="this"/> ∙ <paramref name="mass"/> }.</summary>
    public Force3 Multiply(Mass mass) => Force3.From(mass, this);
    /// <summary>Computes <see cref="Force3"/> according to { <paramref name="acceleration"/> ∙ <paramref name="mass"/> }.</summary>
    public static Force3 operator *(Acceleration3 acceleration, Mass mass) => acceleration.Multiply(mass);

    /// <summary>Computes <see cref="Jerk3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Jerk3 Divide(Time time) => Jerk3.From(this, time);
    /// <summary>Computes <see cref="Jerk3"/> according to { <paramref name="acceleration"/> / <paramref name="time"/> }.</summary>
    public static Jerk3 operator /(Acceleration3 acceleration, Time time) => acceleration.Divide(time);
}
