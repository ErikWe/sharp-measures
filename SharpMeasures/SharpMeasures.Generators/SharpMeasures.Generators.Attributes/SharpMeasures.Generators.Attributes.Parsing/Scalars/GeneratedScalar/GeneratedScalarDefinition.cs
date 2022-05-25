namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class GeneratedScalarDefinition : AAttributeDefinition<GeneratedScalarParsingData, GeneratedScalarLocations>
{
    internal static GeneratedScalarDefinition Empty => new();

    public NamedType Unit { get; init; }
    public NamedType Vector { get; init; }

    public bool Biased { get; init; }

    public string DefaultUnitName { get; init; } = string.Empty;
    public string DefaultUnitSymbol { get; init; } = string.Empty;

    public bool GenerateDocumentation { get; init; }

    private GeneratedScalarDefinition() : base(GeneratedScalarParsingData.Empty) { }
}