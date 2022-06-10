namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class OffsetUnitParsingData : AUnitParsingData
{
    public static OffsetUnitParsingData Empty { get; } = new();

    private OffsetUnitParsingData() { }
}
