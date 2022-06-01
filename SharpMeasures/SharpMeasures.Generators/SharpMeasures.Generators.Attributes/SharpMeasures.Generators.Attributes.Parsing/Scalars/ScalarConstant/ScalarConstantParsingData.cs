namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstantParsingData
{
    internal static ScalarConstantParsingData Empty { get; } = new();

    public string? InterpretedMultiplesName { get; init; }

    private ScalarConstantParsingData() { }
}