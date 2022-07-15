namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Quantities;

public abstract record class AUnresolvedQuantityConstantDefinition<TLocations> : AAttributeDefinition<TLocations>, IUnresolvedQuantityConstant
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public string Name { get; }
    public string Unit { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? Multiples { get; }

    protected AUnresolvedQuantityConstantDefinition(string name, string unit, bool generateMultiplesProperty, string? multiples, TLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;

        GenerateMultiplesProperty = generateMultiplesProperty;
        Multiples = multiples;
    }
}
