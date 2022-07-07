namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.Abstractions;

internal record class RawSpecializedSharpMeasuresScalarDefinition :
    ARawAttributeDefinition<RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations>
{
    public static RawSpecializedSharpMeasuresScalarDefinition Empty => new();

    public NamedType? OriginalScalar { get; init; }

    public bool InheritDerivations { get; init; }
    public bool InheritConstants { get; init; }
    public bool InheritBases { get; init; }
    public bool InheritUnits { get; init; }

    public NamedType? Vector { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public NamedType? Reciprocal { get; init; }
    public NamedType? Square { get; init; }
    public NamedType? Cube { get; init; }
    public NamedType? SquareRoot { get; init; }
    public NamedType? CubeRoot { get; init; }

    public bool GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresScalarDefinition Definition => this;

    private RawSpecializedSharpMeasuresScalarDefinition() : base(SpecializedSharpMeasuresScalarLocations.Empty) { }
}
