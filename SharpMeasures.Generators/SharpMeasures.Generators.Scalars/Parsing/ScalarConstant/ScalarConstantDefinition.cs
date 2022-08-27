namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal record class ScalarConstantDefinition : AQuantityConstantDefinition<ScalarConstantLocations>, IScalarConstant
{
    public double Value { get; }

    public ScalarConstantDefinition(string name, string unit, double value, bool generateMultiplesProperty, string? multiples, ScalarConstantLocations locations)
        : base(name, unit, generateMultiplesProperty, multiples, locations)
    {
        Value = value;
    }
}
