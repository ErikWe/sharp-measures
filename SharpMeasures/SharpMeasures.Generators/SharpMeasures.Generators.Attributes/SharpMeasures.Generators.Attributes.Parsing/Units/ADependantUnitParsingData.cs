namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public abstract record class ADependantUnitParsingData<TLocations> : AUnitParsingData<TLocations>, IDependantUnitParsingData
    where TLocations : ADependantUnitLocations
{
    IDependantUnitLocations IDependantUnitParsingData.Locations => Locations;

    protected ADependantUnitParsingData(TLocations locations) : base(locations) { }
}
