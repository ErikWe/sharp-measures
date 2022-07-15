namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Unresolved.Units;

public record class QuantityConstantResolutionContext : SimpleProcessingContext, IQuantityConstantResolutionContext
{
    public IUnresolvedUnitType Unit { get; }

    public QuantityConstantResolutionContext(DefinedType type, IUnresolvedUnitType unit) : base(type)
    {
        Unit = unit;
    }
}
