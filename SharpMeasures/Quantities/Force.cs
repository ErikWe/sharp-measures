namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Force
{
    /// <summary>Computes <see cref="Force"/> according to { <paramref name="mass"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Force From(Mass mass, Acceleration acceleration) => new(mass.Magnitude * acceleration.Magnitude);

    /// <summary>Computes <see cref="Impulse"/> according to { <see langword="this"/> ∙ <paramref name="time"/> }.</summary>
    public Impulse Multiply(Time time) => Impulse.From(this, time);
    /// <summary>Computes <see cref="Impulse"/> according to { <paramref name="force"/> ∙ <paramref name="time"/> }.</summary>
    public static Impulse operator *(Force force, Time time) => force.Multiply(time);

    /// <summary>Computes <see cref="Work"/> according to { <see langword="this"/> ∙ <paramref name="distance"/> }.</summary>
    public Work Multiply(Distance distance) => Work.From(this, distance);
    /// <summary>Computes <see cref="Work"/> according to { <paramref name="force"/> ∙ <paramref name="distance"/> }.</summary>
    public static Work operator *(Force force, Distance distance) => force.Multiply(distance);

    /// <summary>Computes average <see cref="Pressure"/> according to { <see langword="this"/> / <paramref name="area"/> }.</summary>
    public Pressure Divide(Area area) => Pressure.From(this, area);
    /// <summary>Computes average <see cref="Pressure"/> according to { <paramref name="force"/> / <paramref name="area"/> }.</summary>
    public static Pressure operator /(Force force, Area area) => force.Divide(area);

    /// <summary>Computes average <see cref="Yank"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public Yank Divide(Time time) => Yank.From(this, time);
    /// <summary>Computes average <see cref="Yank"/> according to { <paramref name="force"/> / <paramref name="time"/> }.</summary>
    public static Yank operator /(Force force, Time time) => force.Divide(time);
}
