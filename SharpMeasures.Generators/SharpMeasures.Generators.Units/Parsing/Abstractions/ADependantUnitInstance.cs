namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract record class ADependantUnitInstance<TLocations> : AUnitInstance<TLocations>, IModifiedUnitInstance
    where TLocations : IModifiedUnitInstanceLocations
{
    public string OriginalUnitInstance { get; }

    IModifiedUnitInstanceLocations IModifiedUnitInstance.Locations => Locations;

    protected ADependantUnitInstance(string name, string pluralForm, string originalUnitInstance, TLocations locations) : base(name, pluralForm, locations)
    {
        OriginalUnitInstance = originalUnitInstance;
    }
}
