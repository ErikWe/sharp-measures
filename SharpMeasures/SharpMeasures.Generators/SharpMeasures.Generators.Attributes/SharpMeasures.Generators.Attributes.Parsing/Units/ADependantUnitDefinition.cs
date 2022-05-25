namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class ADependantUnitDefinition<TParsingData, TLocations> : AUnitDefinition<TParsingData, TLocations>, IDependantUnitDefinition
    where TParsingData : ADependantUnitParsingData<TLocations>
    where TLocations : ADependantUnitLocations
{
    public string DependantOn { get; init; } = string.Empty;

    IDependantUnitParsingData IDependantUnitDefinition.ParsingData => ParsingData;

    protected ADependantUnitDefinition(TParsingData parsingData) : base(parsingData) { }
}
