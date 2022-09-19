namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SharpMeasuresScalarLocations : AAttributeLocations<SharpMeasuresScalarLocations>, IScalarBaseLocations, IDefaultUnitInstanceLocations
{
    public static SharpMeasuresScalarLocations Empty => new();

    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Vector { get; init; }

    public MinimalLocation? UseUnitBias { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }
    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitInstanceName { get; init; }
    public MinimalLocation? DefaultUnitInstanceSymbol { get; init; }

    public MinimalLocation? Reciprocal { get; init; }
    public MinimalLocation? Square { get; init; }
    public MinimalLocation? Cube { get; init; }
    public MinimalLocation? SquareRoot { get; init; }
    public MinimalLocation? CubeRoot { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetVector => Vector is not null;

    public bool ExplicitlySetUseUnitBias => UseUnitBias is not null;

    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;

    public bool ExplicitlySetDefaultUnitInstanceName => DefaultUnitInstanceName is not null;
    public bool ExplicitlySetDefaultUnitInstanceSymbol => DefaultUnitInstanceSymbol is not null;

    public bool ExplicitlySetReciprocal => Reciprocal is not null;
    public bool ExplicitlySetSquare => Square is not null;
    public bool ExplicitlySetCube => Cube is not null;
    public bool ExplicitlySetSquareRoot => SquareRoot is not null;
    public bool ExplicitlySetCubeRoot => CubeRoot is not null;

    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SharpMeasuresScalarLocations Locations => this;

    private SharpMeasuresScalarLocations() { }
}
