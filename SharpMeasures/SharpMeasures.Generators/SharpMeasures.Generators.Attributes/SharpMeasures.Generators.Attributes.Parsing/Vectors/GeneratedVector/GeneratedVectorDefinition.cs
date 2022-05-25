namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class GeneratedVectorDefinition : AAttributeDefinition<GeneratedVectorParsingData, GeneratedVectorLocations>
{
    internal static GeneratedVectorDefinition Empty => new();

    public NamedType Unit { get; init; }
    public NamedType Scalar { get; init; }

    public string DefaultUnitName { get; init; } = string.Empty;
    public string DefaultUnitSymbol { get; init; } = string.Empty;

    public bool GenerateDocumentation { get; init; }

    private GeneratedVectorDefinition() : base(GeneratedVectorParsingData.Empty) { }
}