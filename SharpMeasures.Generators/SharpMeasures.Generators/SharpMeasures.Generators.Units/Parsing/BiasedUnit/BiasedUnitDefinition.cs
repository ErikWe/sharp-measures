namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class BiasedUnitDefinition : ADependantUnitDefinition<BiasedUnitLocations>, IBiasedUnit
{
    public IUnresolvedUnitInstance From => DependantOn;

    public string Expression { get; }

    public BiasedUnitDefinition(string name, string plural, IUnresolvedUnitInstance from, string expression, BiasedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
