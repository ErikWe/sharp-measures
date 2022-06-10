namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal record class VectorConstantParsingData
{
    public static VectorConstantParsingData Empty { get; } = new();

    public string? InterpretedMultiplesName { get; init; }

    private VectorConstantParsingData() { }
}
