namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AQuantityConstantLocations<TLocations> : AAttributeLocations<TLocations>, IQuantityConstantLocations
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public MinimalLocation? Name { get; init; }
    public MinimalLocation? UnitInstanceName { get; init; }

    public MinimalLocation? GenerateMultiplesProperty { get; init; }
    public MinimalLocation? Multiples { get; init; }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetUnitInstanceName => UnitInstanceName is not null;

    public bool ExplicitlySetGenerateMultiplesProperty => GenerateMultiplesProperty is not null;
    public bool ExplicitlySetMultiples => Multiples is not null;
}
