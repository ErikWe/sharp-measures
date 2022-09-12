namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal record class ScalarConstantLocations : AQuantityConstantLocations<ScalarConstantLocations>, IScalarConstantLocations
{
    public static ScalarConstantLocations Empty { get; } = new();

    public MinimalLocation? Value { get; init; }

    public bool ExplicitlySetValue => Value is not null;

    protected override ScalarConstantLocations Locations => this;

    private ScalarConstantLocations() { }
}
