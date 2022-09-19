namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public record class DerivedQuantityProcessingContext : SimpleProcessingContext, IDerivedQuantityProcessingContext
{
    public QuantityType ResultingQuantityType { get; }

    public DerivedQuantityProcessingContext(DefinedType type, QuantityType resultingQuantityType) : base(type)
    {
        ResultingQuantityType = resultingQuantityType;
    }
}
