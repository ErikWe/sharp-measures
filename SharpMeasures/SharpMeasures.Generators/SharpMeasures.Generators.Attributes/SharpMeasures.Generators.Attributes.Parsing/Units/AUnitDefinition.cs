namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class AUnitDefinition<TParsingData, TLocations> : AAttributeDefinition<TParsingData, TLocations>, IUnitDefinition
    where TParsingData : AUnitParsingData<TLocations>
    where TLocations : AUnitLocations
{
    public string Name { get; init; } = string.Empty;
    public string Plural { get; init; } = string.Empty;

    IUnitParsingData IUnitDefinition.ParsingData => ParsingData;

    protected AUnitDefinition(TParsingData parsingData) : base(parsingData) { }
}
