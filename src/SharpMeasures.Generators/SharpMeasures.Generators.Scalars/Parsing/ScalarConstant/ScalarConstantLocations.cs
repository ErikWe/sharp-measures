namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal sealed record class ScalarConstantLocations : AQuantityConstantLocations<ScalarConstantLocations>, IScalarConstantLocations
{
    public static ScalarConstantLocations Empty { get; } = new();

    public MinimalLocation? Value { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetValue => Value is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    protected override ScalarConstantLocations Locations => this;

    private ScalarConstantLocations() { }
}
