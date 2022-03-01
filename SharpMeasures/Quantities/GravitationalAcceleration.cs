namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct GravitationalAcceleration
{
    /// <summary>Computes <see cref="GravitationalAcceleration"/> according to { <paramref name="weight"/> / <paramref name="mass"/> }.</summary>
    public static GravitationalAcceleration From(Weight weight, Mass mass) => new(weight.Magnitude / mass.Magnitude);

    /// <summary>Computes <see cref="Weight"/> according to { <see langword="this"/> ∙ <paramref name="mass"/> }.</summary>
    public Weight Multiply(Mass mass) => Weight.From(mass, this);
    /// <summary>Computes <see cref="Weight"/> according to { <paramref name="gravitationalAcceleration"/> ∙ <paramref name="mass"/> }.</summary>
    public static Weight operator *(GravitationalAcceleration gravitationalAcceleration, Mass mass) => gravitationalAcceleration.Multiply(mass);
}
