namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators;

public interface IScaledUnitInstance : IModifiedUnitInstance
{
    public abstract double? Scale { get; }
    public abstract string? Expression { get; }

    new public abstract IScaledUnitInstanceLocations Locations { get; }
}

public interface IScaledUnitInstanceLocations : IModifiedUnitInstanceLocations
{
    public abstract MinimalLocation? Scale { get; }
    public abstract MinimalLocation? Expression { get; }

    public abstract bool ExplicitlySetScale { get; }
    public abstract bool ExplicitlySetExpression { get; }
}
