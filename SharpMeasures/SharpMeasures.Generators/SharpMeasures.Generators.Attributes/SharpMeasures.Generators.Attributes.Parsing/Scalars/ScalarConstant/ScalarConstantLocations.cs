namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstantLocations : AAttributeLocations
{
    internal static ScalarConstantLocations Empty { get; } = new();

    public MinimalLocation Name { get; init; }
    public MinimalLocation Value { get; init; }
    public MinimalLocation Unit { get; init; }

    public MinimalLocation GenerateMultiplesProperty { get; init; }
    public MinimalLocation MultiplesName { get; init; }

    private ScalarConstantLocations() { }
}
