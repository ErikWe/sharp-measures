namespace SharpMeasures.Generators.Units.Refinement.GeneratedUnit;

using SharpMeasures.Generators.Scalars;

internal readonly record struct RefinedGeneratedUnitDefinition
{
    public ScalarInterface Quantity { get; }

    public bool SupportsBiasedQuantities { get; }

    public RefinedGeneratedUnitDefinition(ScalarInterface quantity, bool supportsBiasedQuantities)
    {
        Quantity = quantity;

        SupportsBiasedQuantities = supportsBiasedQuantities;
    }
}
