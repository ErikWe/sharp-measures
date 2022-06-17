namespace SharpMeasures.Generators.Units.Refinement.GeneratedUnit;

using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedGeneratedUnitDefinition
{
    public ScalarInterface Quantity { get; }

    public bool BiasTerm { get; }

    public RefinedGeneratedUnitDefinition(ScalarInterface quantity, bool biasTerm)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
    }
}
