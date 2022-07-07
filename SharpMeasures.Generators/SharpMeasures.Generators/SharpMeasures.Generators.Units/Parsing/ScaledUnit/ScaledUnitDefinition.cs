namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Units.UnitInstances;

internal record class ScaledUnitDefinition : ADependantUnitDefinition<ScaledUnitLocations>, IScaledUnit
{
    public IUnresolvedUnitInstance From => DependantOn;

    public string Expression { get; }

    public ScaledUnitDefinition(string name, string plural, IUnresolvedUnitInstance from, string expression, ScaledUnitLocations locations)
        : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
