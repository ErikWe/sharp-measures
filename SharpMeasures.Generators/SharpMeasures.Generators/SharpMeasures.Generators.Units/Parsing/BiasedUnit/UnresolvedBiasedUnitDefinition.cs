namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class UnresolvedBiasedUnitDefinition : AUnresolvedDependantUnitDefinition<BiasedUnitLocations>, IUnresolvedBiasedUnit
{
    public string From => DependantOn;

    public string Expression { get; }

    public UnresolvedBiasedUnitDefinition(string name, string plural, string from, string expression, BiasedUnitLocations locations) : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
