namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpecificVolume
{
    /// <summary>Computes <see cref="SpecificVolume"/> according to { <see cref="SpecificVolume"/> = <paramref name="volume"/> / <paramref name="mass"/> }.</summary>
    public static SpecificVolume From(Volume volume, Mass mass) => new(volume.Magnitude / mass.Magnitude);
}
