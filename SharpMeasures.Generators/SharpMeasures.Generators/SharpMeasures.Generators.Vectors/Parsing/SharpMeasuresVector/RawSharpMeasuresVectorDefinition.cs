namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresVectorDefinition : ARawAttributeDefinition<SharpMeasuresVectorLocations>
{
    public static RawSharpMeasuresVectorDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int Dimension { get; init; }

    public string? DefaultUnitName { get; init; } = string.Empty;
    public string? DefaultUnitSymbol { get; init; } = string.Empty;

    public bool GenerateDocumentation { get; init; }

    private RawSharpMeasuresVectorDefinition() : base(SharpMeasuresVectorLocations.Empty) { }
}
