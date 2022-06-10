namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class PrefixedUnitParsingData : AUnitParsingData
{
    public static PrefixedUnitParsingData Empty { get; } = new();

    private PrefixedUnitParsingData() { }
}
