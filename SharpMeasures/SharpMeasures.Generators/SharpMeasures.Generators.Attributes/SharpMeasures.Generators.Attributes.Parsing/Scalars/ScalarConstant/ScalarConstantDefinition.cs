namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstantDefinition : AAttributeDefinition<ScalarConstantLocations>
{
    public string Name { get; }
    public double Value { get; }
    public string Unit { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? MultiplesName { get; }

    public ScalarConstantDefinition(string name, double value, string unit, bool generateMultiplesProperty, string? multiplesName, ScalarConstantLocations locations)
        : base(locations)
    {
        Name = name;
        Value = value;
        Unit = unit;

        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }
}