namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class GeneratedVectorLocations : AAttributeLocations
{
    internal static GeneratedVectorLocations Empty => new();

    public MinimalLocation Unit { get; init; }
    public MinimalLocation Scalar { get; init; }

    public MinimalLocation DefaultUnitName { get; init; }
    public MinimalLocation DefaultUnitSymbol { get; init; }

    public MinimalLocation GenerateDocumentation { get; init; }

    private GeneratedVectorLocations() { }
}