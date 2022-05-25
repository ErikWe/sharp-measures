namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class CubeRootableScalarLocations : APoweredScalarLocations
{
    internal static CubeRootableScalarLocations Empty { get; } = new();

    private CubeRootableScalarLocations() { }
}