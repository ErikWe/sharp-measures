namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class ARawUnitDefinition<TParsingData, TLocations> : ARawAttributeDefinition<TLocations>, IRawUnitDefinition
    where TParsingData : AUnitParsingData
    where TLocations : AUnitLocations
{
    public string? Name { get; init; }
    public string? Plural { get; init; }

    IUnitLocations IRawUnitDefinition.Locations => Locations;
    public TParsingData ParsingData { get; init; }
    IUnitParsingData IRawUnitDefinition.ParsingData => ParsingData;

    protected ARawUnitDefinition(TLocations locations, TParsingData parsingData) : base(locations)
    {
        ParsingData = parsingData;
    }
}
