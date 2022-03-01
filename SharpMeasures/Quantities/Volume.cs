namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Volume
{
    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="mass"/> / <paramref name="density"/> },
    /// where <paramref name="mass"/> is the <see cref="Mass"/> of an object with average <see cref="Density"/> <paramref name="density"/>.</summary>
    public static Volume From(Mass mass, Density density) => new(mass.Magnitude / density.Magnitude);

    /// <summary>Computes <see cref="Volume"/> according to { <paramref name="area"/> ∙ <paramref name="length"/> }.</summary>
    public static Volume From(Area area, Length length) => new(area.Magnitude * length.Magnitude);

    /// <summary>Computes <see cref="Mass"/> according to { <see langword="this"/> ∙ <paramref name="density"/> }.</summary>
    public Mass Multiply(Density density) => Mass.From(density, this);
    /// <summary>Computes <see cref="Mass"/> according to { <paramref name="volume"/> ∙ <paramref name="density"/> }.</summary>
    public static Mass operator *(Volume volume, Density density) => volume.Multiply(density);

    /// <summary>Computes <see cref="SpecificVolume"/> according to { <see langword="this"/> / <paramref name="mass"/> }.</summary>
    public SpecificVolume Divide(Mass mass) => SpecificVolume.From(this, mass);
    /// <summary>Computes <see cref="SpecificVolume"/> according to { <paramref name="volume"/> / <paramref name="mass"/> }.</summary>
    public static SpecificVolume operator /(Volume volume, Mass mass) => volume.Divide(mass);

    /// <summary>Computes average <see cref="VolumetricFlowRate"/> according to { <see langword="this"/> / <paramref name="time"/> }.</summary>
    public VolumetricFlowRate Divide(Time time) => VolumetricFlowRate.From(this, time);
    /// <summary>Computes average <see cref="VolumetricFlowRate"/> according to { <paramref name="volume"/> / <paramref name="time"/> }.</summary>
    public static VolumetricFlowRate operator /(Volume volume, Time time) => volume.Divide(time);
}
