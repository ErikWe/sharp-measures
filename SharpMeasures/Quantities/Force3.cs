namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Force3
{
    /// <summary>Computes <see cref="Force3"/> according to { <paramref name="mass"/> ∙ <paramref name="acceleration"/> }.</summary>
    public static Force3 From(Mass mass, Acceleration3 acceleration) => new(mass.Magnitude * acceleration.Components);
}
