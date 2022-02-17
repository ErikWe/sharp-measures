namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Weight
{
    /// <summary>Computes <see cref="Weight"/> according to { <paramref name="mass"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public static Weight From(Mass mass, GravitationalAcceleration gravitationalAcceleration) => new(mass.Magnitude * gravitationalAcceleration.Magnitude);
}
