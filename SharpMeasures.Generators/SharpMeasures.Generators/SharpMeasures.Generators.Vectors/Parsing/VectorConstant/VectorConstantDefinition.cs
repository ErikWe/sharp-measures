namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class VectorConstantDefinition : AAttributeDefinition<VectorConstantLocations>
{
    public string Name { get; }
    public string Unit { get; }
    public ReadOnlyEquatableList<double> Value { get; }

    public bool GenerateMultiplesProperty { get; }
    public string? Multiples { get; }

    public VectorConstantDefinition(string name, string unit, IReadOnlyList<double> value, bool generateMultiplesProperty, string? multiples,
        VectorConstantLocations locations)
        : base(locations)
    {
        Name = name;
        Unit = unit;
        Value = value.AsReadOnlyEquatable();

        GenerateMultiplesProperty = generateMultiplesProperty;
        Multiples = multiples;
    }
}
