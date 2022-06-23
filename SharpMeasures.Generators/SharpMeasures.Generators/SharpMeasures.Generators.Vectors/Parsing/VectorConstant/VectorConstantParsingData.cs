namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal record class VectorConstantParsingData
{
    public static VectorConstantParsingData Empty { get; } = new();

    public string? InterpretedMultiples { get; init; }

    private VectorConstantParsingData() { }
}
