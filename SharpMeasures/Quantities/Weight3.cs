namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Weight3
{
    /// <summary>Computes <see cref="Weight3"/> according to { <paramref name="mass"/> ∙ <paramref name="gravitationalAcceleration"/> }.</summary>
    public static Weight3 From(Mass mass, GravitationalAcceleration3 gravitationalAcceleration) => new(mass.Magnitude * gravitationalAcceleration.Components);
}
