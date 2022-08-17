namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Linq;

internal class ConvertibleVectorProcesser : AConvertibleQuantityProcesser<UnresolvedConvertibleVectorDefinition>
{
    public ConvertibleVectorProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnresolvedConvertibleVectorDefinition> Process(IConvertibleQuantityProcessingContext context, UnprocessedConvertibleQuantityDefinition definition)
    {
        var validity = Validate(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedConvertibleVectorDefinition>(allDiagnostics);
        }

        var processed = ProcessQuantities(context, definition);
        allDiagnostics = allDiagnostics.Concat(processed);

        if (processed.Result.Quantities.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedConvertibleVectorDefinition>(allDiagnostics);
        }

        UnresolvedConvertibleVectorDefinition product = new(processed.Result.Quantities, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations, processed.Result.LocationMap);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
