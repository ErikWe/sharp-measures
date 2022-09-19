namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class RawSpecializedSharpMeasuresScalarDefinition : ARawAttributeDefinition<RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSpecializedSharpMeasuresScalarDefinition FromSymbolic(SymbolicSpecializedSharpMeasuresScalarDefinition symbolicDefinition) => new RawSpecializedSharpMeasuresScalarDefinition(symbolicDefinition.Locations) with
    {
        OriginalQuantity = symbolicDefinition.OriginalQuantity?.AsNamedType(),
        InheritDerivations = symbolicDefinition.InheritDerivations,
        InheritConstants = symbolicDefinition.InheritConstants,
        InheritConversions = symbolicDefinition.InheritConversions,
        InheritBases = symbolicDefinition.InheritBases,
        InheritUnits = symbolicDefinition.InheritUnits,
        Vector = symbolicDefinition.Vector?.AsNamedType(),
        ImplementSum = symbolicDefinition.ImplementSum,
        ImplementDifference = symbolicDefinition.ImplementDifference,
        Difference = symbolicDefinition.Difference?.AsNamedType(),
        DefaultUnitInstanceName = symbolicDefinition.DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol = symbolicDefinition.DefaultUnitInstanceSymbol,
        Reciprocal = symbolicDefinition.Reciprocal?.AsNamedType(),
        Square = symbolicDefinition.Square?.AsNamedType(),
        Cube = symbolicDefinition.Cube?.AsNamedType(),
        SquareRoot = symbolicDefinition.SquareRoot?.AsNamedType(),
        CubeRoot = symbolicDefinition.CubeRoot?.AsNamedType(),
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    public NamedType? OriginalQuantity { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritBases { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public NamedType? Vector { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public NamedType? Reciprocal { get; init; }
    public NamedType? Square { get; init; }
    public NamedType? Cube { get; init; }
    public NamedType? SquareRoot { get; init; }
    public NamedType? CubeRoot { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSpecializedSharpMeasuresScalarDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSpecializedSharpMeasuresScalarDefinition(SpecializedSharpMeasuresScalarLocations locations) : base(locations) { }
}
