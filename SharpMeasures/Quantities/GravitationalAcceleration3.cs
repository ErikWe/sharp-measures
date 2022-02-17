namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct GravitationalAcceleration3
{
    /// <summary>Computes <see cref="GravitationalAcceleration3"/> according to { <paramref name="weight"/> / <paramref name="mass"/> }.</summary>
    public static GravitationalAcceleration3 From(Weight3 weight, Mass mass) => new(weight.Components / mass.Magnitude);
}
