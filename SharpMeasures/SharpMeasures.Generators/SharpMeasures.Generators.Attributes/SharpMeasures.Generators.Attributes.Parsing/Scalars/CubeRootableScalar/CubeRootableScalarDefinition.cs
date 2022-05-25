namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class CubeRootableScalarDefinition : APoweredScalarDefinition<CubeRootableScalarParsingData, CubeRootableScalarLocations>
{
    internal static CubeRootableScalarDefinition Empty { get; } = new();

    private CubeRootableScalarDefinition() : base(CubeRootableScalarParsingData.Empty) { }
}