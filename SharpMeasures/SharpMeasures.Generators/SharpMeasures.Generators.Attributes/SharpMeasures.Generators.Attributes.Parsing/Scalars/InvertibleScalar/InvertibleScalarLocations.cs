namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class InvertibleScalarLocations : APoweredScalarLocations
{
    internal static InvertibleScalarLocations Empty { get; } = new();

    private InvertibleScalarLocations() { }
}