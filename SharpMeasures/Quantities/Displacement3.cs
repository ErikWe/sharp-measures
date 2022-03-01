namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Displacement3
{
    /// <summary>Computes <see cref="Displacement3"/> according to { <paramref name="velocity"/> ∙ <paramref name="time"/> },
    /// where <paramref name="velocity"/> is the average <see cref="Velocity3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Displacement3 From(Velocity3 velocity, Time time) => new(velocity.Components * time.Magnitude);

    /// <summary>Computes <see cref="Torque3"/> according to { <see langword="this"/> × <paramref name="force"/> }.</summary>
    public Torque3 Cross(Force3 force) => Torque3.From(this, force);

    /// <summary>Computes final <see cref="Position3"/> according to { <see langword="this"/> + <paramref name="initialPosition"/> }.</summary>
    public Position3 Add(Position3 initialPosition) => Position3.From(initialPosition, this);
    /// <summary>Computes final <see cref="Position3"/> according to { <paramref name="displacement"/> + <paramref name="initialPosition"/> }.</summary>
    public static Position3 operator +(Displacement3 displacement, Position3 initialPosition) => displacement.Add(initialPosition);

    /// <summary>Computes <see cref="Absement3"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Absement3 Multiply(Time time) => Absement3.From(this, time);
    /// <summary>Computes <see cref="Absement3"/> according to { <paramref name="displacement"/> ∙ <paramref name="time"/> }.</summary>
    public static Absement3 operator *(Displacement3 displacement, Time time) => displacement.Multiply(time);

    /// <summary>Computes average <see cref="Velocity3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Velocity3 Divide(Time time) => Velocity3.From(this, time);
    /// <summary>Computes average <see cref="Velocity3"/> according to { <paramref name="displacement"/> / <paramref name="time"/> }.</summary>
    public static Velocity3 operator /(Displacement3 displacement, Time time) => displacement.Divide(time);
}
