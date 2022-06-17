namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class DerivedUnitParsingData : AUnitParsingData
{
    public static DerivedUnitParsingData Empty { get; } = new();

    private DerivedUnitParsingData() { }
}
