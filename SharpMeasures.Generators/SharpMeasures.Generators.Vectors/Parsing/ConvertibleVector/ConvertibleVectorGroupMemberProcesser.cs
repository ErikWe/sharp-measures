namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

internal interface IConvertibleVectorGroupMemberProcessingContext : IConvertibleQuantityProcessingContext
{
    public abstract NamedType Group { get; }
}

internal class ConvertibleVectorGroupMemberrProcesser : AConvertibleQuantityProcesser<IConvertibleVectorGroupMemberProcessingContext, ConvertibleVectorDefinition>
{
    private IConvertibleQuantityProcessingDiagnostics Diagnostics { get; }

    public ConvertibleVectorGroupMemberrProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleVectorGroupMemberProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return Validate(context, definition)
            .Merge(() => ProcessQuantities(context, definition))
            .Transform((scalarsAndLocationMap) => ProduceResult(definition, scalarsAndLocationMap.Quantities, scalarsAndLocationMap.LocationMap));
    }

    private static ConvertibleVectorDefinition ProduceResult(RawConvertibleQuantityDefinition definition, IReadOnlyList<NamedType> scalars, IReadOnlyList<int> locationMap)
    {
        return new(scalars, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations, locationMap);
    }

    protected override IValidityWithDiagnostics ValidateQuantity(IConvertibleVectorGroupMemberProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return base.ValidateQuantity(context, definition, index)
            .Validate(() => ValidateQuantityNotOwnGroup(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateQuantityNotOwnGroup(IConvertibleVectorGroupMemberProcessingContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        var quantityIsNotOwnGroup = definition.Quantities[index]!.Value != context.Group;

        return ValidityWithDiagnostics.Conditional(quantityIsNotOwnGroup, () => Diagnostics.ConvertibleToSelf(context, definition, index));
    }
}
