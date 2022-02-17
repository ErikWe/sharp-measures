namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct LinearDensity
{
    /// <summary>Computes <see cref="LinearDensity"/> according to { <paramref name="mass"/> / <paramref name="length"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> distributed over some <see cref="Length"/> <paramref name="length"/>.</summary>
    public static LinearDensity From(Mass mass, Length length) => new(mass.Magnitude / length.Magnitude);
}
