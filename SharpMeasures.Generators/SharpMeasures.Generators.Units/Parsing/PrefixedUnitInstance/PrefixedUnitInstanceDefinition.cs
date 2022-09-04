namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class PrefixedUnitInstanceDefinition : ADependantUnitInstance<PrefixedUnitInstanceLocations>, IPrefixedUnitInstance
{
    public MetricPrefixName? MetricPrefix { get; }
    public BinaryPrefixName? BinaryPrefix { get; }

    IPrefixedUnitInstanceLocations IPrefixedUnitInstance.Locations => Locations;

    public PrefixedUnitInstanceDefinition(string name, string pluralForm, string originalUnitInstance, MetricPrefixName metricPrefix, PrefixedUnitInstanceLocations locations)
        : base(name, pluralForm, originalUnitInstance, locations)
    {
        MetricPrefix = metricPrefix;
    }

    public PrefixedUnitInstanceDefinition(string name, string pluralForm, string originalUnitInstance, BinaryPrefixName binaryPrefix, PrefixedUnitInstanceLocations locations)
        : base(name, pluralForm, originalUnitInstance, locations)
    {
        BinaryPrefix = binaryPrefix;
    }
}
