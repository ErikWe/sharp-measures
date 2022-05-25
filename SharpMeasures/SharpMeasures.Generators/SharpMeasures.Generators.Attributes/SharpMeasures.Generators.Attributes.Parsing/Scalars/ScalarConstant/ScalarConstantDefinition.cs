namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstantDefinition : AAttributeDefinition<ScalarConstantParsingData, ScalarConstantLocations>
{
    internal static ScalarConstantDefinition Empty { get; } = new();

    public string Name { get; init; } = string.Empty;
    public double Value { get; init; }
    public string Unit { get; init; } = string.Empty;

    public bool GenerateMultiplesProperty { get; init; }
    public string MultiplesName { get; init; } = string.Empty;

    private ScalarConstantDefinition() : base(ScalarConstantParsingData.Empty) { }
}