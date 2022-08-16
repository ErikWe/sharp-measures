namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class RawBiasedUnitDefinition : ARawDependantUnitDefinition<BiasedUnitLocations>, IRawBiasedUnit
{
    public string From => DependantOn;

    public string Expression { get; }

    public RawBiasedUnitDefinition(string name, string plural, string from, string expression, BiasedUnitLocations locations) : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
