namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

public record class VectorConstantParsingData
{
    internal static VectorConstantParsingData Empty { get; } = new();

    public string? InterpretedMultiplesName { get; init; }

    private VectorConstantParsingData() { }
}
