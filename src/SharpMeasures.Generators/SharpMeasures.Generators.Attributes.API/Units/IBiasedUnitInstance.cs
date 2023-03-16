namespace SharpMeasures.Generators.Units;

public interface IBiasedUnitInstance : IModifiedUnitInstance
{
    public abstract double? Bias { get; }
    public abstract string? Expression { get; }

    new public abstract IBiasedUnitInstanceLocations Locations { get; }
}

public interface IBiasedUnitInstanceLocations : IModifiedUnitInstanceLocations
{
    public abstract MinimalLocation? Bias { get; }
    public abstract MinimalLocation? Expression { get; }

    public abstract bool ExplicitlySetBias { get; }
    public abstract bool ExplicitlySetExpression { get; }
}
