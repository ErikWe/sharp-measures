namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Weight3
{
    /// <summary>Computes <see cref="Weight3"/> according to { <paramref name="mass"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public static Weight3 From(Mass mass, GravitationalAcceleration3 gravitationalAcceleration) => new(mass.Magnitude * gravitationalAcceleration.Components);

    /// <summary>Computes <see cref="GravitationalAcceleration3"/> according to { <see langword="this"/> / <paramref name="mass"/> }.</summary>
    public GravitationalAcceleration3 Divide(Mass mass) => GravitationalAcceleration3.From(this, mass);
    /// <summary>Computes <see cref="GravitationalAcceleration3"/> according to { <paramref name="weight"/> / <paramref name="mass"/> }.</summary>
    public static GravitationalAcceleration3 operator /(Weight3 weight, Mass mass) => weight.Divide(mass);
}
