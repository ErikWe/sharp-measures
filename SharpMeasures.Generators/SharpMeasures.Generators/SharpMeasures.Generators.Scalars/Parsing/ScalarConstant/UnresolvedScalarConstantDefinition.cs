namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal record class UnresolvedScalarConstantDefinition : AUnresolvedQuantityConstantDefinition<ScalarConstantLocations>, IUnresolvedScalarConstant
{
    public double Value { get; }

    public UnresolvedScalarConstantDefinition(string name, string unit, double value, bool generateMultiplesProperty, string? multiples, ScalarConstantLocations locations)
        : base(name, unit, generateMultiplesProperty, multiples, locations)
    {
        Value = value;
    }
}
