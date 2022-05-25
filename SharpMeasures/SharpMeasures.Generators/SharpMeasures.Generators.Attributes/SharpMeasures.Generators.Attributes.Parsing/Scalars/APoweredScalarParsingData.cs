namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public abstract record class APoweredScalarParsingData<TLocations> : AAttributeParsingData<TLocations>, IPoweredScalarParsingData
    where TLocations : APoweredScalarLocations
{
    IPoweredScalarLocations IPoweredScalarParsingData.Locations => Locations;

    protected APoweredScalarParsingData(TLocations locations) : base(locations) { }
}
