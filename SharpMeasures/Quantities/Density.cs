namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Density
{
    /// <summary>Computes average <see cref="Density"/> according to { <paramref name="mass"/> / <paramref name="volume"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> distributed over some <see cref="Volume"/> <paramref name="volume"/>.</summary>
    public static Density From(Mass mass, Volume volume) => new(mass.Magnitude / volume.Magnitude);

    /// <summary>Computes <see cref="Mass"/> according to { <see langword="this"/> ∙ <paramref name="volume"/> }.</summary>
    public Mass Multiply(Volume volume) => Mass.From(this, volume);
    /// <summary>Computes <see cref="Mass"/> according to { <paramref name="density"/> ∙ <paramref name="volume"/> }.</summary>
    public static Mass operator *(Density density, Volume volume) => density.Multiply(volume);
}
