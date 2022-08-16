namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class ScalarConstantDefinition : AQuantityConstantDefinition<ScalarConstantLocations>, IScalarConstant
{
    public double Value { get; }

    public ScalarConstantDefinition(string name, IRawUnitInstance unit, double value, bool generateMultiplesProperty, string? multiples,
        ScalarConstantLocations locations)
        : base(name, unit, generateMultiplesProperty, multiples, locations)
    {
        Value = value;
    }
}
