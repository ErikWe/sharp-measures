namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Force3
{
    /// <summary>Computes <see cref="Force3"/> according to { <paramref name="mass"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Force3 From(Mass mass, Acceleration3 acceleration) => new(mass.Magnitude * acceleration.Components);

    /// <summary>Computes <see cref="Impulse3"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Impulse3 Multiply(Time time) => Impulse3.From(this, time);
    /// <summary>Computes <see cref="Impulse3"/> according to { <paramref name="force"/> ∙ <paramref name="time"/> }.</summary>
    public static Impulse3 operator *(Force3 force, Time time) => force.Multiply(time);

    /// <summary>Computes average <see cref="Yank3"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Yank3 Divide(Time time) => Yank3.From(this, time);
    /// <summary>Computes average <see cref="Yank3"/> according to { <paramref name="force"/> / <paramref name="time"/> }.</summary>
    public static Yank3 operator /(Force3 force, Time time) => force.Divide(time);
}
