namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class RawSpecializedSharpMeasuresScalarDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations>, IDefaultUnitDefinition
{
    public static RawSpecializedSharpMeasuresScalarDefinition Empty => new();

    public NamedType? OriginalScalar { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritBases { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public NamedType? Vector { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitName { get; init; }
    public string? DefaultUnitSymbol { get; init; }

    public NamedType? Reciprocal { get; init; }
    public NamedType? Square { get; init; }
    public NamedType? Cube { get; init; }
    public NamedType? SquareRoot { get; init; }
    public NamedType? CubeRoot { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresScalarDefinition Definition => this;

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    private RawSpecializedSharpMeasuresScalarDefinition() : base(SpecializedSharpMeasuresScalarLocations.Empty) { }
}
