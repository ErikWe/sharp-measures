namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class RawSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresVectorGroupDefinition FromSymbolic(SymbolicSharpMeasuresVectorGroupDefinition symbolicDefinition) => new RawSharpMeasuresVectorGroupDefinition(symbolicDefinition.Locations) with
    {
        Unit = symbolicDefinition.Unit?.AsNamedType(),
        Scalar = symbolicDefinition.Scalar?.AsNamedType(),
        ImplementSum = symbolicDefinition.ImplementSum,
        ImplementDifference = symbolicDefinition.ImplementDifference,
        Difference = symbolicDefinition.Difference?.AsNamedType(),
        DefaultUnitInstanceName = symbolicDefinition.DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol = symbolicDefinition.DefaultUnitInstanceSymbol
    };

    public NamedType? Unit { get; init; }
    public NamedType? Scalar { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;

    public NamedType? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    protected override RawSharpMeasuresVectorGroupDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSharpMeasuresVectorGroupDefinition(SharpMeasuresVectorGroupLocations locations) : base(locations) { }
}
