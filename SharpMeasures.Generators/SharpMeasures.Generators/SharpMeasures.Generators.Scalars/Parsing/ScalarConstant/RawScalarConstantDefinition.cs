namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawScalarConstantDefinition : ARawAttributeDefinition<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public static RawScalarConstantDefinition Empty { get; } = new();

    public string? Name { get; init; }
    public string? Unit { get; init; }
    public double Value { get; init; }

    public bool GenerateMultiplesProperty { get; init; }
    public string? Multiples { get; init; }

    public ScalarConstantParsingData ParsingData { get; init; } = ScalarConstantParsingData.Empty;

    protected override RawScalarConstantDefinition Definition => this;

    private RawScalarConstantDefinition() : base(ScalarConstantLocations.Empty) { }
}
