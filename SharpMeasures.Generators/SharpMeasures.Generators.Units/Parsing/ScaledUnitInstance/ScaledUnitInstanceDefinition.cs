namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class ScaledUnitInstanceDefinition : ADependantUnitInstance<ScaledUnitInstanceLocations>, IScaledUnitInstance
{
    public double? Scale { get; }
    public string? Expression { get; }

    IScaledUnitInstanceLocations IScaledUnitInstance.Locations => Locations;

    public ScaledUnitInstanceDefinition(string name, string pluralForm, string originalUnitInstance, string expression, ScaledUnitInstanceLocations locations)
        : base(name, pluralForm, originalUnitInstance, locations)
    {
        Expression = expression;
    }

    public ScaledUnitInstanceDefinition(string name, string pluralForm, string originalUnitInstance, double value, ScaledUnitInstanceLocations locations)
        : base(name, pluralForm, originalUnitInstance, locations)
    {
        Scale = value;
    }
}
