namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpatialFrequency :
    IAddableScalarQuantity<SpatialFrequency, SpatialFrequency>,
    ISubtractableScalarQuantity<SpatialFrequency, SpatialFrequency>
{
    public static SpatialFrequency From(Mass mass, LinearDensity linearDensity) => new(mass.Magnitude * linearDensity.Magnitude);
}
