namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Mass :
    IAddableScalarQuantity<Mass, Mass>,
    ISubtractableScalarQuantity<Mass, Mass>
{
    public static Mass From(LinearDensity linearDensity, Length length) => new(linearDensity.Magnitude * length.Magnitude);
    public static Mass From(LinearDensity linearDensity, SpatialFrequency spatialFrequency) => new(linearDensity.Magnitude / spatialFrequency.Magnitude);
    public static Mass From(SurfaceDensity surfaceDensity, Area area) => new(surfaceDensity.Magnitude * area.Magnitude);
    public static Mass From(Density density, Volume volume) => new(density.Magnitude * volume.Magnitude);
    public static Mass From(Volume volume, SpecificVolume specificVolume) => new(volume.Magnitude / specificVolume.Magnitude);

    public static Mass From(MassFlowRate massFlowRate, Time time) => new(massFlowRate.Magnitude * time.Magnitude);
    public static Mass From(MassFlowRate massFlowRate, Frequency frequency) => new(massFlowRate.Magnitude / frequency.Magnitude);
}
