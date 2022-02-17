namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Pressure
{
    /// <summary>Computes average <see cref="Pressure"/> according to { <paramref name="force"/> / <paramref name="area"/> },
    /// where <paramref name="force"/> is the <see cref="Force"/> applied over some <see cref="Area"/> <paramref name="area"/>.</summary>
    public static Pressure From(Force force, Area area) => new(force.Magnitude / area.Magnitude);
}
