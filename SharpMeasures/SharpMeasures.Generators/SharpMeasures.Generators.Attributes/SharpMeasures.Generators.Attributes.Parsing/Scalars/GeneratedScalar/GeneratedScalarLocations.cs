namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class GeneratedScalarLocations : AAttributeLocations
{
    internal static GeneratedScalarLocations Empty => new();

    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Vector { get; init; }

    public MinimalLocation? Biased { get; init; }

    public MinimalLocation? DefaultUnitName { get; init; }
    public MinimalLocation? DefaultUnitSymbol { get; init; }

    public MinimalLocation? Reciprocal { get; init; }
    public MinimalLocation? Square { get; init; }
    public MinimalLocation? Cube { get; init; }
    public MinimalLocation? SquareRoot { get; init; }
    public MinimalLocation? CubeRoot { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetVector => Vector is not null;
    public bool ExplicitlySetBiased => Biased is not null;
    public bool ExplicitlySetDefaultUnitName => DefaultUnitName is not null;
    public bool ExplicitlySetDefaultUnitSymbol => DefaultUnitSymbol is not null;
    public bool ExplicitlySetReciprocal => Reciprocal is not null;
    public bool ExplicitlySetSquare => Square is not null;
    public bool ExplicitlySetCube => Cube is not null;
    public bool ExplicitlySetSquareRoot => SquareRoot is not null;
    public bool ExplicitlySetCubeRoot => CubeRoot is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private GeneratedScalarLocations() { }
}