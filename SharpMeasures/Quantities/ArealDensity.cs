namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct ArealDensity
{
    /// <summary>Computes average <see cref="ArealDensity"/> according to { <paramref name="mass"/> / <paramref name="area"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> distributed over some <see cref="Area"/> <paramref name="area"/>.</summary>
    public static ArealDensity From(Mass mass, Area area) => new(mass.Magnitude / area.Magnitude);
}
