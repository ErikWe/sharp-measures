namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal sealed record class ScalarConstantDefinition : AQuantityConstantDefinition<ScalarConstantLocations>, IScalarConstant
{
    public double? Value { get; }
    public string? Expression { get; }

    IScalarConstantLocations IScalarConstant.Locations => Locations;

    public ScalarConstantDefinition(string name, string unitInstanceName, double? value, string? expression, bool generateMultiplesProperty, string? multiples, ScalarConstantLocations locations) : base(name, unitInstanceName, generateMultiplesProperty, multiples, locations)
    {
        Value = value;
        Expression = expression;
    }
}
