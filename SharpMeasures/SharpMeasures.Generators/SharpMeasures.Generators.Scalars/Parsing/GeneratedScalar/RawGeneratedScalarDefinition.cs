namespace SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawGeneratedScalarDefinition : ARawAttributeDefinition<GeneratedScalarLocations>
{
    public static RawGeneratedScalarDefinition Empty => new();

    public NamedType? Unit { get; init; }
    public NamedType? Vector { get; init; }

    public bool Biased { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public NamedType? Reciprocal { get; init; }
    public NamedType? Square { get; init; }
    public NamedType? Cube { get; init; }
    public NamedType? SquareRoot { get; init; }
    public NamedType? CubeRoot { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawGeneratedScalarDefinition() : base(GeneratedScalarLocations.Empty) { }
}
