namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

internal class ConvertibleVectorProcesser : AConvertibleQuantityProcesser<IConvertibleQuantityProcessingContext, ConvertibleVectorDefinition>
{
    public ConvertibleVectorProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return Validate(context, definition)
            .Merge(() => ProcessQuantities(context, definition))
            .Transform((scalarsAndLocationMap) => ProduceResult(definition, scalarsAndLocationMap.Quantities, scalarsAndLocationMap.LocationMap));
    }

    private static ConvertibleVectorDefinition ProduceResult(RawConvertibleQuantityDefinition definition, IReadOnlyList<NamedType> scalars, IReadOnlyList<int> locationMap)
    {
        return new(scalars, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations, locationMap);
    }
}
