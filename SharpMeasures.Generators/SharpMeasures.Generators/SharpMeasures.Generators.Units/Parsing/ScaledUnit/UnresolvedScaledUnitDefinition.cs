namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class UnresolvedScaledUnitDefinition : AUnresolvedDependantUnitDefinition<ScaledUnitLocations>, IUnresolvedScaledUnit
{
    public string From => DependantOn;

    public string Expression { get; }

    public UnresolvedScaledUnitDefinition(string name, string plural, string from, string expression, ScaledUnitLocations locations) : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
