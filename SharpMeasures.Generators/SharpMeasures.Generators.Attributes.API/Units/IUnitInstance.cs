namespace SharpMeasures.Generators.Units;

public interface IUnitInstance : IAttributeDefinition
{
    public abstract string Name { get; }
    public abstract string PluralForm { get; }

    new public abstract IUnitInstanceLocations Locations { get; }
}

public interface IUnitInstanceLocations : IAttributeLocations
{
    public abstract MinimalLocation? Name { get; }
    public abstract MinimalLocation? PluralForm { get; }

    public abstract bool ExplicitlySetName { get; }
    public abstract bool ExplicitlySetPluralForm { get; }
}
