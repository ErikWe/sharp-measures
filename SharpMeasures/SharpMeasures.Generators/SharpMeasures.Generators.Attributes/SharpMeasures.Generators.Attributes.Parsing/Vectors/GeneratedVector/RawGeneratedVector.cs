namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class RawGeneratedVector : ARawAttributeDefinition<GeneratedVectorLocations>
{
    internal static RawGeneratedVector Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int Dimension { get; init; }

    public string? DefaultUnitName { get; init; } = string.Empty;
    public string? DefaultUnitSymbol { get; init; } = string.Empty;

    public bool GenerateDocumentation { get; init; }

    private RawGeneratedVector() : base(GeneratedVectorLocations.Empty) { }
}
