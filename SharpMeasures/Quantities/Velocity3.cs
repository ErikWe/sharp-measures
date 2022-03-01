namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Velocity3
{
    /// <summary>Computes average <see cref="Velocity3"/> according to { <paramref name="displacement"/> / <paramref name="time"/> },
    /// where <paramref name="displacement"/> is the <see cref="Displacement3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Velocity3 From(Displacement3 displacement, Time time) => new(displacement.Components / time.Magnitude);

    /// <summary>Computes <see cref="Displacement3"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Displacement3 Multiply(Time time) => Displacement3.From(this, time);
    /// <summary>Computes <see cref="Displacement3"/> according to { <paramref name="velocity"/> ∙ <paramref name="time"/> }.</summary>
    public static Displacement3 operator *(Velocity3 velocity, Time time) => velocity.Multiply(time);

    /// <summary>Computes <see cref="Momentum3"/> according to { <see langword="this"/> ∙ <paramref name="mass"/> }.</summary>
    public Momentum3 Multiply(Mass mass) => Momentum3.From(mass, this);
    /// <summary>Computes <see cref="Momentum3"/> according to { <paramref name="velocity"/> ∙ <paramref name="mass"/> }.</summary>
    public static Momentum3 operator *(Velocity3 velocity, Mass mass) => velocity.Multiply(mass);

    /// <summary>Computes average <see cref="Acceleration3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Acceleration3 Divide(Time time) => Acceleration3.From(this, time);
    /// <summary>Computes average <see cref="Acceleration3"/> according to { <paramref name="velocity"/> / <paramref name="time"/> }.</summary>
    public static Acceleration3 operator /(Velocity3 velocity, Time time) => velocity.Divide(time);
}
