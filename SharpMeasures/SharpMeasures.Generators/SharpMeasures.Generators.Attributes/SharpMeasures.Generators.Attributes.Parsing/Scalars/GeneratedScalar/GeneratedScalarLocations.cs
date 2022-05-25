namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class GeneratedScalarLocations : AAttributeLocations
{
    internal static GeneratedScalarLocations Empty => new();

    public MinimalLocation Unit { get; init; }
    public MinimalLocation Vector { get; init; }

    public MinimalLocation Biased { get; init; }

    public MinimalLocation DefaultUnitName { get; init; }
    public MinimalLocation DefaultUnitSymbol { get; init; }

    public MinimalLocation GenerateDocumentation { get; init; }

    private GeneratedScalarLocations() { }
}