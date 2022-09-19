namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public sealed record class DerivedQuantityProcessingContext : IDerivedQuantityProcessingContext
{
    public DefinedType Type { get; }

    public QuantityType ResultingQuantityType { get; }

    public DerivedQuantityProcessingContext(DefinedType type, QuantityType resultingQuantityType)
    {
        Type = type;

        ResultingQuantityType = resultingQuantityType;
    }
}
