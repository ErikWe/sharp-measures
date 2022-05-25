namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class CubeRootableScalarParsingData : APoweredScalarParsingData<CubeRootableScalarLocations>
{
    internal static CubeRootableScalarParsingData Empty { get; } = new();

    private CubeRootableScalarParsingData() : base(CubeRootableScalarLocations.Empty) { }
}