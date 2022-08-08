namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Linq;

internal class ConvertibleScalarProcesser : AConvertibleQuantityProcesser<UnresolvedConvertibleScalarDefinition>
{
    public ConvertibleScalarProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnresolvedConvertibleScalarDefinition> Process(IConvertibleQuantityProcessingContext context,
        RawConvertibleQuantityDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedConvertibleScalarDefinition>(allDiagnostics);
        }

        var processed = ProcessQuantities(context, definition);
        allDiagnostics = allDiagnostics.Concat(processed);

        if (processed.Result.Quantities.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedConvertibleScalarDefinition>(allDiagnostics);
        }

        UnresolvedConvertibleScalarDefinition product = new(processed.Result.Quantities, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations, processed.Result.LocationMap);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
