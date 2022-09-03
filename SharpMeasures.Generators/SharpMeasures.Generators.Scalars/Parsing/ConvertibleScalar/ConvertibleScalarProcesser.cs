namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

internal class ConvertibleScalarProcesser : AConvertibleQuantityProcesser<IConvertibleQuantityProcessingContext, ConvertibleScalarDefinition>
{
    public ConvertibleScalarProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ConvertibleScalarDefinition> Process(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return Validate(context, definition)
            .Merge(() => ProcessQuantities(context, definition))
            .Transform((scalarsAndLocationMap) => ProduceResult(definition, scalarsAndLocationMap.Quantities, scalarsAndLocationMap.LocationMap));
    }

    private static ConvertibleScalarDefinition ProduceResult(RawConvertibleQuantityDefinition definition, IReadOnlyList<NamedType> scalars, IReadOnlyList<int> locationMap)
    {
        return new(scalars, definition.ConversionDirection, definition.CastOperatorBehaviour, definition.Locations, locationMap);
    }
}
