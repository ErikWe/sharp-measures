namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawGeneratedVectorDefinition : ARawAttributeDefinition<GeneratedVectorLocations>
{
    public static RawGeneratedVectorDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int Dimension { get; init; }

    public string? DefaultUnitName { get; init; } = string.Empty;
    public string? DefaultUnitSymbol { get; init; } = string.Empty;

    public bool GenerateDocumentation { get; init; }

    private RawGeneratedVectorDefinition() : base(GeneratedVectorLocations.Empty) { }
}
