namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpatialFrequency
{
    /// <summary>Computes <see cref="SpatialFrequency"/> according to { <paramref name="linearDensity"/> / <paramref name="mass"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object with average <see cref="LinearDensity"/> <paramref name="linearDensity"/>.</summary>
    public static SpatialFrequency From(LinearDensity linearDensity, Mass mass) => new(linearDensity.Magnitude / mass.Magnitude);
}
