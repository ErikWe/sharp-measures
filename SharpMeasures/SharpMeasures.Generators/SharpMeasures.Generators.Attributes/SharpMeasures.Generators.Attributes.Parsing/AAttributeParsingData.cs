namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeParsingData<TLocations> : IAttributeParsingData
    where TLocations : IAttributeLocations
{
    public TLocations Locations { get; init; }
    IAttributeLocations IAttributeParsingData.Locations => Locations;

    protected AAttributeParsingData(TLocations locations)
    {
        Locations = locations;
    }
}
