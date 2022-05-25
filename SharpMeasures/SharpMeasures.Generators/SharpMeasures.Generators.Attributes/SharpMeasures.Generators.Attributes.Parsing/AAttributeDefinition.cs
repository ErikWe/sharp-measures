namespace SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AAttributeDefinition<TParsingData, TLocations> : IAttributeDefinition
    where TParsingData : IAttributeParsingData
    where TLocations : IAttributeLocations
{
    public TParsingData ParsingData { get; init; }
    IAttributeParsingData IAttributeDefinition.ParsingData => ParsingData;

    protected AAttributeDefinition(TParsingData parsingData)
    {
        ParsingData = parsingData;
    }
}
