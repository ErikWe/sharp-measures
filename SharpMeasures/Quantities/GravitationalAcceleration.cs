namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct GravitationalAcceleration
{
    /// <summary>Computes <see cref="GravitationalAcceleration"/> according to { <paramref name="weight"/> / <paramref name="mass"/> }.</summary>
    public static GravitationalAcceleration From(Weight weight, Mass mass) => new(weight.Magnitude / mass.Magnitude);
}
