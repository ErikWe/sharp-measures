namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class ARawDependantUnitDefinition<TParsingData, TLocations> : ARawUnitDefinition<TParsingData, TLocations>, IRawDependantUnitDefinition
    where TParsingData : AUnitParsingData
    where TLocations : ADependantUnitLocations
{
    public string? DependantOn { get; init; }

    IDependantUnitLocations IRawDependantUnitDefinition.Locations => Locations;

    protected ARawDependantUnitDefinition(TLocations locations, TParsingData parsingData) : base(locations, parsingData) { }
}
