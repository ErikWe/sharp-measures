namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class SquarableScalarLocations : APoweredScalarLocations
{
    internal static SquarableScalarLocations Empty { get; } = new();

    private SquarableScalarLocations() { }
}