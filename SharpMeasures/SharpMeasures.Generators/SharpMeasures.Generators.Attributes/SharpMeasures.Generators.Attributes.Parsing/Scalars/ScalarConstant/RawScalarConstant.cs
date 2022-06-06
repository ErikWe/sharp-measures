namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class RawScalarConstant : ARawAttributeDefinition<ScalarConstantLocations>
{
    internal static RawScalarConstant Empty { get; } = new();

    public string? Name { get; init; }
    public string? Unit { get; init; }
    public double Value { get; init; }

    public bool GenerateMultiplesProperty { get; init; }
    public string? MultiplesName { get; init; }

    public ScalarConstantParsingData ParsingData { get; init; } = ScalarConstantParsingData.Empty;

    private RawScalarConstant() : base(ScalarConstantLocations.Empty) { }
}
