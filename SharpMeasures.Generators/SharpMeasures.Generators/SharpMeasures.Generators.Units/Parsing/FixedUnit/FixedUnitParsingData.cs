namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class FixedUnitParsingData : AUnitParsingData
{
    internal static FixedUnitParsingData Empty { get; } = new();

    private FixedUnitParsingData() { }
}
