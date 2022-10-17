namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarConstant : IQuantityConstant
{
    public abstract double? Value { get; }
    public abstract string? Expression { get; }

    new public abstract IScalarConstantLocations Locations { get; }
}

public interface IScalarConstantLocations : IQuantityConstantLocations
{
    public abstract MinimalLocation? Value { get; }
    public abstract MinimalLocation? Expression { get; }

    public abstract bool ExplicitlySetValue { get; }
    public abstract bool ExplicitlySetExpression { get; }
}
