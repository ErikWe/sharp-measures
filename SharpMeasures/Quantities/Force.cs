namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Force
{
    /// <summary>Computes <see cref="Force"/> according to { <paramref name="mass"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Force From(Mass mass, Acceleration acceleration) => new(mass.Magnitude * acceleration.Magnitude);
}
