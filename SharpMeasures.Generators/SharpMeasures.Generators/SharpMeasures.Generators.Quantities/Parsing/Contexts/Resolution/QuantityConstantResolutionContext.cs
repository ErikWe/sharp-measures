namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Raw.Units;

public record class QuantityConstantResolutionContext : SimpleProcessingContext, IQuantityConstantResolutionContext
{
    public IRawUnitType Unit { get; }

    public QuantityConstantResolutionContext(DefinedType type, IRawUnitType unit) : base(type)
    {
        Unit = unit;
    }
}
