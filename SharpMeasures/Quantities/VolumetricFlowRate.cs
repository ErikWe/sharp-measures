namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct VolumetricFlowRate
{
    /// <summary>Computes average <see cref="VolumetricFlowRate"/> according to { <paramref name="volume"/> / <paramref name="time"/> },
    /// where <paramref name="volume"/> is the change in <see cref="Volume"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static VolumetricFlowRate From(Volume volume, Time time) => new(volume.Magnitude / time.Magnitude);
}
