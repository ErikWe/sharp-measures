namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class BiasedUnitInstanceDefinition : ADependantUnitInstance<BiasedUnitInstanceLocations>, IBiasedUnitInstance
{
    public double? Bias { get; }
    public string? Expression { get; }

    IBiasedUnitInstanceLocations IBiasedUnitInstance.Locations => Locations;

    public BiasedUnitInstanceDefinition(string name, string pluralForm, string originalUnitInstance, string expression, BiasedUnitInstanceLocations locations)
        : base(name, pluralForm, originalUnitInstance, locations)
    {
        Expression = expression;
    }

    public BiasedUnitInstanceDefinition(string name, string pluralForm, string originalUnitInstance, double bias, BiasedUnitInstanceLocations locations)
        : base(name, pluralForm, originalUnitInstance, locations)
    {
        Bias = bias;
    }
}
