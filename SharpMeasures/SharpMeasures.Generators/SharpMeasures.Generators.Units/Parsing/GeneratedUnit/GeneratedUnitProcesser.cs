namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal interface IGeneratedUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullQuantity(IProcessingContext context, RawGeneratedUnitDefinition definition);
}

internal class GeneratedUnitProcesser : AProcesser<IProcessingContext, RawGeneratedUnitDefinition, GeneratedUnitDefinition>
{
    private IGeneratedUnitProcessingDiagnostics Diagnostics { get; }

    public GeneratedUnitProcesser(IGeneratedUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedUnitDefinition> Process(IProcessingContext context, RawGeneratedUnitDefinition definition)
    {
        if (definition.Quantity is null)
        {
            return OptionalWithDiagnostics.Empty<GeneratedUnitDefinition>(Diagnostics.NullQuantity(context, definition));
        }

        GeneratedUnitDefinition product = new(definition.Quantity.Value, definition.SupportsBiasedQuantities, definition.GenerateDocumentation, definition.Locations);
        return OptionalWithDiagnostics.Result(product);
    }
}
