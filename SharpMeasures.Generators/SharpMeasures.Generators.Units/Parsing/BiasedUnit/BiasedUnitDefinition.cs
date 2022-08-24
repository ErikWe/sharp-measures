namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;

internal record class BiasedUnitDefinition : ADependantUnitDefinition<BiasedUnitLocations>, IBiasedUnit
{
    public string From => DependantOn;

    public string Expression { get; }

    public BiasedUnitDefinition(string name, string plural, string from, string expression, BiasedUnitLocations locations)
        : base(name, plural, from, locations)
    {
        Expression = expression;
    }
}
