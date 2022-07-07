namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SpecializedSharpMeasuresScalarLocations : AAttributeLocations<SpecializedSharpMeasuresScalarLocations>
{
    public static SpecializedSharpMeasuresScalarLocations Empty => new();

    public MinimalLocation? OriginalScalar { get; init; }

    public MinimalLocation? InheritDerivations { get; init; }
    public MinimalLocation? InheritConstants { get; init; }
    public MinimalLocation? InheritBases { get; init; }
    public MinimalLocation? InheritUnits { get; init; }

    public MinimalLocation? Vector { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }

    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitName { get; init; }
    public MinimalLocation? DefaultUnitSymbol { get; init; }

    public MinimalLocation? Reciprocal { get; init; }
    public MinimalLocation? Square { get; init; }
    public MinimalLocation? Cube { get; init; }
    public MinimalLocation? SquareRoot { get; init; }
    public MinimalLocation? CubeRoot { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetOriginalScalar => OriginalScalar is not null;
    public bool ExplicitlySetInheritDerivations => InheritDerivations is not null;
    public bool ExplicitlySetInheritConstants => InheritConstants is not null;
    public bool ExplicitlySetInheritBases => InheritBases is not null;
    public bool ExplicitlySetInheritUnits => InheritUnits is not null;
    public bool ExplicitlySetVector => Vector is not null;
    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;
    public bool ExplicitlySetDefaultUnitName => DefaultUnitName is not null;
    public bool ExplicitlySetDefaultUnitSymbol => DefaultUnitSymbol is not null;
    public bool ExplicitlySetReciprocal => Reciprocal is not null;
    public bool ExplicitlySetSquare => Square is not null;
    public bool ExplicitlySetCube => Cube is not null;
    public bool ExplicitlySetSquareRoot => SquareRoot is not null;
    public bool ExplicitlySetCubeRoot => CubeRoot is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SpecializedSharpMeasuresScalarLocations Locations => this;

    private SpecializedSharpMeasuresScalarLocations() { }
}
