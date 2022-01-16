namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Area :
    IAddableScalarQuantity<Area, Area>,
    ISubtractableScalarQuantity<Area, Area>
{
    public static Area From(Mass mass, SurfaceDensity surfaceDensity) => new(mass.Magnitude / surfaceDensity.Magnitude);
}
