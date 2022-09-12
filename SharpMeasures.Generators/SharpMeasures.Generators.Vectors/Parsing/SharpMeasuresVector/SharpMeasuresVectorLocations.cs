namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class SharpMeasuresVectorLocations : AAttributeLocations<SharpMeasuresVectorLocations>, IVectorBaseLocations, IDefaultUnitInstanceLocations
{
    public static SharpMeasuresVectorLocations Empty => new();

    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Scalar { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }
    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitInstanceName { get; init; }
    public MinimalLocation? DefaultUnitInstanceSymbol { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetScalar => Scalar is not null;

    public bool ExplicitlySetDimension => Dimension is not null;

    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;

    public bool ExplicitlySetDefaultUnitInstanceName => DefaultUnitInstanceName is not null;
    public bool ExplicitlySetDefaultUnitInstanceSymbol => DefaultUnitInstanceSymbol is not null;

    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SharpMeasuresVectorLocations Locations => this;

    private SharpMeasuresVectorLocations() { }
}
