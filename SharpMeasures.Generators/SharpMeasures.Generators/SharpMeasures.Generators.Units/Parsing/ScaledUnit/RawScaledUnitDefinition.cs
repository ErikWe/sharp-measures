namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class RawScaledUnitDefinition : ARawDependantUnitDefinition<ScaledUnitLocations>, IRawScaledUnit
{
    public string From => DependantOn;

    public string Expression { get; }

    public RawScaledUnitDefinition(string name, string plural, string from, string expression, ScaledUnitLocations locations) : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
