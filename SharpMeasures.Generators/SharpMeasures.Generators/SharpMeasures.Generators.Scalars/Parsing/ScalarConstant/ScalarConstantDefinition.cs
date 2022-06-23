namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ScalarConstantDefinition : AAttributeDefinition<ScalarConstantLocations>
{
    public string Name { get; }
    public string Unit { get; }
    public double Value { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? Multiples { get; }

    public ScalarConstantDefinition(string name, string unit, double value, bool generateMultiplesProperty, string? multiples, ScalarConstantLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;
        Value = value;

        GenerateMultiplesProperty = generateMultiplesProperty;
        Multiples = multiples;
    }
}
