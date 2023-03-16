namespace SharpMeasures.Generators.Quantities;

public interface IQuantityConstant : IAttributeDefinition
{
    public abstract string Name { get; }
    public abstract string UnitInstanceName { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }

    new public abstract IQuantityConstantLocations Locations { get; }
}

public interface IQuantityConstantLocations : IAttributeLocations
{
    public abstract MinimalLocation? Name { get; }
    public abstract MinimalLocation? UnitInstanceName { get; }

    public abstract MinimalLocation? GenerateMultiplesProperty { get; }
    public abstract MinimalLocation? Multiples { get; }

    public abstract bool ExplicitlySetName { get; }
    public abstract bool ExplicitlySetUnitInstanceName { get; }

    public abstract bool ExplicitlySetGenerateMultiplesProperty { get; }
    public abstract bool ExplicitlySetMultiples { get; }
}
