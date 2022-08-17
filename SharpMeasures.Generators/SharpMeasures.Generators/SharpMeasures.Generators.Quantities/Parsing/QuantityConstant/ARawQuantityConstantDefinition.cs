namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Quantities;

public abstract record class ARawQuantityConstantDefinition<TLocations> : AAttributeDefinition<TLocations>, IRawQuantityConstant
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public string Name { get; }
    public string Unit { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? Multiples { get; }

    protected ARawQuantityConstantDefinition(string name, string unit, bool generateMultiplesProperty, string? multiples, TLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;

        GenerateMultiplesProperty = generateMultiplesProperty;
        Multiples = multiples;
    }
}
