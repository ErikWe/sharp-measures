namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class ScaledUnitParsingData : AUnitParsingData
{
    public static ScaledUnitParsingData Empty { get; } = new();

    private ScaledUnitParsingData() { }
}
