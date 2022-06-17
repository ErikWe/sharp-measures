namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal record class ScalarConstantParsingData
{
    public static ScalarConstantParsingData Empty { get; } = new();

    public string? InterpretedMultiplesName { get; init; }

    private ScalarConstantParsingData() { }
}
