namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Volume :
    IAddableScalarQuantity<Volume, Volume>,
    ISubtractableScalarQuantity<Volume, Volume>
{
    public static Volume From(Area area, SpatialFrequency spatialFrequency) => new(area.Magnitude / spatialFrequency.Magnitude);

    public static Volume From(Mass mass, Density density) => new(mass.Magnitude / density.Magnitude);
    public static Volume From(Mass mass, SpecificVolume specificVolume) => new(mass.Magnitude * specificVolume.Magnitude);

    public static Volume From(VolumetricFlowRate volumetricFlowRate, Time time) => new(volumetricFlowRate.Magnitude * time.Magnitude);
    public static Volume From(VolumetricFlowRate volumetricFlowRate, Frequency frequency) => new(volumetricFlowRate.Magnitude / frequency.Magnitude);
}
