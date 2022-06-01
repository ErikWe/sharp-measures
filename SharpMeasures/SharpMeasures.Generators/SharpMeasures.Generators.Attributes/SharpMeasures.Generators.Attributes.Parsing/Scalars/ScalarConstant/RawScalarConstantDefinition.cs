namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class RawScalarConstantDefinition : ARawAttributeDefinition<ScalarConstantLocations>
{
    internal static RawScalarConstantDefinition Empty { get; } = new();

    public string? Name { get; init; }
    public double Value { get; init; }
    public string? Unit { get; init; }

    public bool GenerateMultiplesProperty { get; init; }
    public string? MultiplesName { get; init; }

    public ScalarConstantParsingData ParsingData { get; init; } = ScalarConstantParsingData.Empty;

    private RawScalarConstantDefinition() : base(ScalarConstantLocations.Empty) { }
}