namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Mass
{
    /// <summary>Computes <see cref="Mass"/> according to { <paramref name="density"/> / <paramref name="volume"/> },
    /// where <paramref name="density"/> is the average <see cref="Density"/> of an object with <see cref="Volume"/> <paramref name="volume"/>.</summary>
    public static Mass From(Density density, Volume volume) => new(density.Magnitude / volume.Magnitude);
}
