namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Density
{
    /// <summary>Computes average <see cref="Density"/> according to { <see cref="Density"/> = <paramref name="mass"/> / <paramref name="volume"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> distributed over some <see cref="Volume"/> <paramref name="volume"/>.</summary>
    public static Density From(Mass mass, Volume volume) => new(mass.Magnitude / volume.Magnitude);
}
