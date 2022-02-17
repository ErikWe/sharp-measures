namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Volume
{
    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="mass"/> / <paramref name="density"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object with average <see cref="Density"/> <paramref name="density"/>.</summary>
    public static Volume From(Mass mass, Density density) => new(mass.Magnitude / density.Magnitude);

    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="area"/> ∙ <paramref name="length"/> }.</summary>
    public static Volume From(Area area, Length length) => new(area.Magnitude * length.Magnitude);
}
