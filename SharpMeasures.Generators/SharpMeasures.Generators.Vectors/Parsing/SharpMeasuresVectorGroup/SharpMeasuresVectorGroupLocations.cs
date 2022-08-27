namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class SharpMeasuresVectorGroupLocations : AAttributeLocations<SharpMeasuresVectorGroupLocations>, IDefaultUnitLocations
{
    public static SharpMeasuresVectorGroupLocations Empty => new();

    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Scalar { get; init; }

    public MinimalLocation? ImplementSum { get; init; }
    public MinimalLocation? ImplementDifference { get; init; }
    public MinimalLocation? Difference { get; init; }

    public MinimalLocation? DefaultUnitName { get; init; }
    public MinimalLocation? DefaultUnitSymbol { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetScalar => Scalar is not null;
    public bool ExplicitlySetImplementSum => ImplementSum is not null;
    public bool ExplicitlySetImplementDifference => ImplementDifference is not null;
    public bool ExplicitlySetDifference => Difference is not null;
    public bool ExplicitlySetDefaultUnitName => DefaultUnitName is not null;
    public bool ExplicitlySetDefaultUnitSymbol => DefaultUnitSymbol is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SharpMeasuresVectorGroupLocations Locations => this;

    private SharpMeasuresVectorGroupLocations() { }
}
