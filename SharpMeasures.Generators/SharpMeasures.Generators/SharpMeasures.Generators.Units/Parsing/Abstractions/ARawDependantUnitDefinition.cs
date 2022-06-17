namespace SharpMeasures.Generators.Units.Parsing.Abstractions;
internal abstract record class ARawDependantUnitDefinition<TParsingData, TLocations> : ARawUnitDefinition<TParsingData, TLocations>, IRawDependantUnitDefinition
    where TParsingData : AUnitParsingData
    where TLocations : ADependantUnitLocations
{
    public string? DependantOn { get; init; }

    IDependantUnitLocations IRawDependantUnitDefinition.Locations => Locations;

    protected ARawDependantUnitDefinition(TLocations locations, TParsingData parsingData) : base(locations, parsingData) { }
}
