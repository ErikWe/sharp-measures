namespace SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

public abstract record class AQuantityConstantDefinition<TLocations> : AAttributeDefinition<TLocations>, IQuantityConstant
    where TLocations : AQuantityConstantLocations<TLocations>
{
    public string Name { get; }
    public IRawUnitInstance Unit { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? Multiples { get; }

    protected AQuantityConstantDefinition(string name, IRawUnitInstance unit, bool generateMultiplesProperty, string? multiples,
        TLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;

        GenerateMultiplesProperty = generateMultiplesProperty;
        Multiples = multiples;
    }
}
