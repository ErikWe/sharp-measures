namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct GravitationalAcceleration3
{
    /// <summary>Computes <see cref="GravitationalAcceleration3"/> according to { <paramref name="weight"/> / <paramref name="mass"/> }.</summary>
    public static GravitationalAcceleration3 From(Weight3 weight, Mass mass) => new(weight.Components / mass.Magnitude);

    /// <summary>Computes <see cref="Weight3"/> according to { <see langword="this"/> ∙ <paramref name="mass"/> }.</summary>
    public Weight3 Multiply(Mass mass) => Weight3.From(mass, this);
    /// <summary>Computes <see cref="Weight3"/> according to { <paramref name="gravitationalAcceleration"/> ∙ <paramref name="mass"/> }.</summary>
    public static Weight3 operator *(GravitationalAcceleration3 gravitationalAcceleration, Mass mass) => gravitationalAcceleration.Multiply(mass);
}
