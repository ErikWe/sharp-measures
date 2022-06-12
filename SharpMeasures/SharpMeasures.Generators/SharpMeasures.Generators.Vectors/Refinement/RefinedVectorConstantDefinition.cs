namespace SharpMeasures.Generators.Vectors.Refinement;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct RefinedVectorConstantDefinition
{
    public string Name { get; }
    public UnitInstance Unit { get; }
    public ReadOnlyEquatableList<double> Value { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? MultiplesName { get; }

    public RefinedVectorConstantDefinition(string name, UnitInstance unit, IReadOnlyList<double> value, bool generateMultiplesProperty, string? multiplesName)
    {
        Name = name;
        Unit = unit;
        Value = value.AsReadOnlyEquatable();

        GenerateMultiplesProperty = generateMultiplesProperty;
        MultiplesName = multiplesName;
    }
}
