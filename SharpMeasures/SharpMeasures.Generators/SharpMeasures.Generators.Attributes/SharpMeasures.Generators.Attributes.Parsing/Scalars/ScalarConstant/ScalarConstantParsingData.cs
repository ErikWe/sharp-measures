namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class ScalarConstantParsingData : AAttributeParsingData<ScalarConstantLocations>
{
    internal static ScalarConstantParsingData Empty { get; } = new();

    public string InterpretedMultiplesName { get; init; } = string.Empty;

    private ScalarConstantParsingData() : base(ScalarConstantLocations.Empty) { }
}