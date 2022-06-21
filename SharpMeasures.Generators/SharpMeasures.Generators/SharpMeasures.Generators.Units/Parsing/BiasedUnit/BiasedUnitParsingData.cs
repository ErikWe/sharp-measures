namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class BiasedUnitParsingData : AUnitParsingData
{
    public static BiasedUnitParsingData Empty { get; } = new();

    private BiasedUnitParsingData() { }
}
