namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;
using System.Linq;

internal class ScalarConstantProcesser
    : AQuantityConstantProcesser<IQuantityConstantProcessingContext, RawScalarConstantDefinition, ScalarConstantLocations, UnresolvedScalarConstantDefinition>
{
    public ScalarConstantProcesser(IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnresolvedScalarConstantDefinition> Process(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedScalarConstantDefinition>();
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedScalarConstantDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return product.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<UnresolvedScalarConstantDefinition> ProcessDefinition(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        UnresolvedScalarConstantDefinition product = new(definition.Name!, definition.Unit!, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }
}
