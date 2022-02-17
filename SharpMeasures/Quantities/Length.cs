namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Length
{
    /// <summary>Computes <see cref="Length"/> according to { <paramref name="mass"/> / <paramref name="linearDensity"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object with average <see cref="LinearDensity"/> <paramref name="linearDensity"/>.</summary>
    public static Length From(Mass mass, LinearDensity linearDensity) => new(mass.Magnitude / linearDensity.Magnitude);
}
