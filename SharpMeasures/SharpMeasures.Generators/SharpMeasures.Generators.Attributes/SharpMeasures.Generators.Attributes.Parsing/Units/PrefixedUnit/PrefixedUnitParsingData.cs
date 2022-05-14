namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public readonly record struct PrefixedUnitParsingData(PrefixedUnitParsingData.PrefixType SpecifiedPrefixType = PrefixedUnitParsingData.PrefixType.None)
{
    public enum PrefixType { None, Metric, Binary }
}