namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct LinearDensity
{
    /// <summary>Computes <see cref="LinearDensity"/> according to { <paramref name="mass"/> / <paramref name="length"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> distributed over some <see cref="Length"/> <paramref name="length"/>.</summary>
    public static LinearDensity From(Mass mass, Length length) => new(mass.Magnitude / length.Magnitude);

    /// <summary>Computes <see cref="SpatialFrequency"/> according to { <see langword="this"/> / <paramref name="mass"/> }.</summary>
    public SpatialFrequency Divide(Mass mass) => SpatialFrequency.From(this, mass);
    /// <summary>Computes <see cref="SpatialFrequency"/> according to { <paramref name="linearDensity"/> / <paramref name="mass"/> }.</summary>
    public static SpatialFrequency operator /(LinearDensity linearDensity, Mass mass) => linearDensity.Divide(mass);
}
