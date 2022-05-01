namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public readonly record struct PrefixedUnitParameters(string Name, string Plural, string From, MetricPrefixName MetricPrefixName,
    BinaryPrefixName BinaryPrefixName, PrefixedUnitParameters.PrefixType SpecifiedPrefixType)
    : IUnitDefinitionParameters, IDependantUnitDefinitionParameters
{
    public enum PrefixType { None, Metric, Binary }

    string IDependantUnitDefinitionParameters.DependantOn => From;
}