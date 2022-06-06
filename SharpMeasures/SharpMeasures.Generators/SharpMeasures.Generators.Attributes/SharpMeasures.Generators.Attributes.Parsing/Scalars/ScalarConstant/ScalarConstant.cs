namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstant : AAttributeDefinition<ScalarConstantLocations>
{
    public string Name { get; }
    public string Unit { get; }
    public double Value { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? MultiplesName { get; }

    public ScalarConstant(string name, string unit, double value, bool generateMultiplesProperty, string? multiplesName, ScalarConstantLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;
        Value = value;

        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }
}
