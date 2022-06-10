namespace SharpMeasures.Generators.Scalars.Refinement;

using SharpMeasures.Generators.Units;

internal readonly record struct RefinedScalarConstantDefinition
{
    public string Name { get; }
    public UnitInstance Unit { get; }
    public double Value { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? MultiplesName { get; }

    public RefinedScalarConstantDefinition(string name, UnitInstance unit, double value, bool generateMultiplesProperty, string? multiplesName)
    {
        Name = name;
        Unit = unit;
        Value = value;

        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }
}
