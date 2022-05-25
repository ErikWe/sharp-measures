namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class SquareRootableScalarLocations : APoweredScalarLocations
{
    internal static SquareRootableScalarLocations Empty { get; } = new();

    private SquareRootableScalarLocations() { }
}