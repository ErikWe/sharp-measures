namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class CubableScalarLocations : APoweredScalarLocations
{
    internal static CubableScalarLocations Empty { get; } = new();

    private CubableScalarLocations() { }
}