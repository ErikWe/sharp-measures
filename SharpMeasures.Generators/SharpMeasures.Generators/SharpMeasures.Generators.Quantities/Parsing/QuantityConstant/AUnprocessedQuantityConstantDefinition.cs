namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AUnprocessedQuantityConstantDefinition<TDefinition, TLocations> : AUnprocessedAttributeDefinition<TDefinition, TLocations>
    where TDefinition : AUnprocessedQuantityConstantDefinition<TDefinition, TLocations>
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public string? Name { get; init; }
    public string? Unit { get; init; }

    public bool GenerateMultiplesProperty { get; init; }
    public string? Multiples { get; init; }

    protected AUnprocessedQuantityConstantDefinition(TLocations locations) : base(locations) { }
}
