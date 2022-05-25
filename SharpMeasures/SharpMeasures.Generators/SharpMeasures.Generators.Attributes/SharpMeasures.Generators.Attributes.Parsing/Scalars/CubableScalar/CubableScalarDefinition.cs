namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class CubableScalarDefinition : APoweredScalarDefinition<CubableScalarParsingData, CubableScalarLocations>
{
    internal static CubableScalarDefinition Empty { get; } = new();

    private CubableScalarDefinition() : base(CubableScalarParsingData.Empty) { }
}