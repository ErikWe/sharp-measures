namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;

public abstract record class AQuantityConstantDefinition<TLocations> : AAttributeDefinition<TLocations>, IQuantityConstant
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public string Name { get; }
    public string UnitInstanceName { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? Multiples { get; }

    IQuantityConstantLocations IQuantityConstant.Locations => Locations;

    protected AQuantityConstantDefinition(string name, string unitInstanceName, bool generateMultiplesProperty, string? multiples, TLocations locations)
        : base(locations)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;

        GenerateMultiplesProperty = generateMultiplesProperty;
        Multiples = multiples;
    }
}
