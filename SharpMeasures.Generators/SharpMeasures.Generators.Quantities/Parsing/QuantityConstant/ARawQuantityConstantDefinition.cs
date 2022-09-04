namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;

public abstract record class ARawQuantityConstantDefinition<TDefinition, TLocations> : ARawAttributeDefinition<TDefinition, TLocations>
    where TDefinition : ARawQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public string? Name { get; init; }
    public string? UnitInstanceName { get; init; }

    public bool GenerateMultiplesProperty { get; init; }
    public string? Multiples { get; init; } = CommonPluralNotations.PrependMultiplesOf;

    protected ARawQuantityConstantDefinition(TLocations locations) : base(locations) { }
}
