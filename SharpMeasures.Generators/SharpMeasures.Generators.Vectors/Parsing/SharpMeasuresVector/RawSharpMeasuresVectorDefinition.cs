namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class RawSharpMeasuresVectorDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresVectorDefinition FromSymbolic(SymbolicSharpMeasuresVectorDefinition symbolicDefinition) => new RawSharpMeasuresVectorDefinition(symbolicDefinition.Locations) with
    {
        Unit = symbolicDefinition.Unit?.AsNamedType(),
        Scalar = symbolicDefinition.Scalar?.AsNamedType(),
        Dimension = symbolicDefinition.Dimension,
        ImplementSum = symbolicDefinition.ImplementSum,
        ImplementDifference = symbolicDefinition.ImplementDifference,
        Difference = symbolicDefinition.Difference?.AsNamedType(),
        DefaultUnitInstanceName = symbolicDefinition.DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol = symbolicDefinition.DefaultUnitInstanceSymbol,
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public int? Dimension { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;
    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSharpMeasuresVectorDefinition(SharpMeasuresVectorLocations locations) : base(locations) { }
}
