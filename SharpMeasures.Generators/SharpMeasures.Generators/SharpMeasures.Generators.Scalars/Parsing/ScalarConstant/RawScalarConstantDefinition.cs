namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal record class RawScalarConstantDefinition : AUnprocessedQuantityConstantDefinition<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public static RawScalarConstantDefinition Empty { get; } = new();

    public double Value { get; init; }

    protected override RawScalarConstantDefinition Definition => this;

    private RawScalarConstantDefinition() : base(ScalarConstantLocations.Empty) { }
}
