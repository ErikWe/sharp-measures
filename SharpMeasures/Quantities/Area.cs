namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Area
{
    /// <summary>Computes <see cref="Area"/> according to { <paramref name="mass"/> / <paramref name="arealDensity"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object with average <see cref="ArealDensity"/> <paramref name="arealDensity"/>.</summary>
    public static Area From(Mass mass, ArealDensity arealDensity) => new(mass.Magnitude / arealDensity.Magnitude);

    /// <summary>Computes <see cref="Volume"/> according to { <see langword="this"/> ∙ <paramref name="length"/> }.</summary>
    public Volume Multiply(Length length) => Volume.From(this, length);
    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="area"/> ∙ <paramref name="length"/> }.</summary>
    public static Volume operator *(Area area, Length length) => area.Multiply(length);
}
