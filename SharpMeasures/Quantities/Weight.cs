namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Weight
{
    /// <summary>Computes <see cref="Weight"/> according to { <paramref name="mass"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public static Weight From(Mass mass, GravitationalAcceleration gravitationalAcceleration) => new(mass.Magnitude * gravitationalAcceleration.Magnitude);

    /// <summary>Computes <see cref="GravitationalEnergy"/> according to { <see langword="this"/> ∙ <paramref name="distance"/> }.</summary>
    public GravitationalEnergy Multiply(Distance distance) => GravitationalEnergy.From(this, distance);
    /// <summary>Computes <see cref="GravitationalEnergy"/> according to { <paramref name="weight"/> ∙ <paramref name="distance"/> }.</summary>
    public static GravitationalEnergy operator *(Weight weight, Distance distance) => weight.Multiply(distance);

    /// <summary>Computes <see cref="GravitationalAcceleration"/> according to { <see langword="this"/> / <paramref name="mass"/> }.</summary>
    public GravitationalAcceleration Divide(Mass mass) => GravitationalAcceleration.From(this, mass);
    /// <summary>Computes <see cref="GravitationalAcceleration"/> according to { <paramref name="weight"/> / <paramref name="mass"/> }.</summary>
    public static GravitationalAcceleration operator /(Weight weight, Mass mass) => weight.Divide(mass);
}
