namespace SharpMeasures.Generators.Scalars.Processing;

using SharpMeasures.Generators.Units;

internal readonly record struct ProcessedScalarConstant
{
    public string Name { get; }
    public double Value { get; }
    public UnitInstance Unit { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? MultiplesName { get; }

    public ProcessedScalarConstant(string name, double value, UnitInstance unit, bool generateMultiplesProperty, string? multiplesName)
    {
        Name = name;
        Value = value;
        Unit = unit;

        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }
}
