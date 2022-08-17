namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;
using System.Linq;

internal class VectorConstantProcesser
    : AQuantityConstantProcesser<IQuantityConstantProcessingContext, RawVectorConstantDefinition, VectorConstantLocations, UnresolvedVectorConstantDefinition>
{
    public VectorConstantProcesser(IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<UnresolvedVectorConstantDefinition> Process(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedVectorConstantDefinition>();
        }

        var validity = Validate(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedVectorConstantDefinition>(allDiagnostics);
        }

        var product = ProcessDefinition(context, definition);
        allDiagnostics = allDiagnostics.Concat(product);

        return OptionalWithDiagnostics.Result(product.Result, allDiagnostics);
    }

    private IResultWithDiagnostics<UnresolvedVectorConstantDefinition> ProcessDefinition(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        UnresolvedVectorConstantDefinition product = new(definition.Name!, definition.Unit!, definition.Value, processedMultiplesPropertyData.Result.Generate,
            processedMultiplesPropertyData.Result.Name, definition.Locations);

        return ResultWithDiagnostics.Construct(product, processedMultiplesPropertyData.Diagnostics);
    }
}
