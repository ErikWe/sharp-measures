namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawVectorConstantDefinition : ARawAttributeDefinition<VectorConstantLocations>
{
    public static RawVectorConstantDefinition Empty { get; } = new();

    public string? Name { get; init; }
    public string? Unit { get; init; }
    public ReadOnlyEquatableList<double> Value { get; init; } = ReadOnlyEquatableList<double>.Empty;

    public bool GenerateMultiplesProperty { get; init; }
    public string? Multiples { get; init; }

    public VectorConstantParsingData ParsingData { get; init; } = VectorConstantParsingData.Empty;

    private RawVectorConstantDefinition() : base(VectorConstantLocations.Empty) { }
}
