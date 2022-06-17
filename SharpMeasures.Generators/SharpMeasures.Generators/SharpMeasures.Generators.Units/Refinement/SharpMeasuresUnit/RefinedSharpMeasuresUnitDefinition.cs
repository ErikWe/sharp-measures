namespace SharpMeasures.Generators.Units.Refinement.SharpMeasuresUnit;

using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedSharpMeasuresUnitDefinition
{
    public ScalarInterface Quantity { get; }

    public bool BiasTerm { get; }

    public RefinedSharpMeasuresUnitDefinition(ScalarInterface quantity, bool biasTerm)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
    }
}
