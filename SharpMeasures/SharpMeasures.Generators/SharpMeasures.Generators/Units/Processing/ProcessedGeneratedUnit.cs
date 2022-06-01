namespace SharpMeasures.Generators.Units.Processing;

using SharpMeasures.Generators.Scalars;

internal readonly record struct ProcessedGeneratedUnit
{
    public ScalarInterface Quantity { get; }

    public bool SupportsBiasedQuantities { get; }

    public ProcessedGeneratedUnit(ScalarInterface quantity, bool supportsBiasedQuantities)
    {
        Quantity = quantity;

        SupportsBiasedQuantities = supportsBiasedQuantities;
    }
}
