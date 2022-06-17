namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedVectorLocations : AAttributeLocations
{
    public static GeneratedVectorLocations Empty => new();

    public MinimalLocation? Unit { get; init; }
    public MinimalLocation? Scalar { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? DefaultUnitName { get; init; }
    public MinimalLocation? DefaultUnitSymbol { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetUnit => Unit is not null;
    public bool ExplicitlySetScalar => Scalar is not null;
    public bool ExplicitlySetDimension => Dimension is not null;
    public bool ExplicitlySetDefaultUnitName => DefaultUnitName is not null;
    public bool ExplicitlySetDefaultUnitSymbol => DefaultUnitSymbol is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private GeneratedVectorLocations() { }
}
