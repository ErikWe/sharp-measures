namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;

public interface IGeneratedUnitDiagnostics
{
    public abstract Diagnostic? NullQuantity(IProcessingContext context, RawGeneratedUnitDefinition definition);
}

public class GeneratedUnitProcesser : AProcesser<IProcessingContext, RawGeneratedUnitDefinition, GeneratedUnitDefinition>
{
    private IGeneratedUnitDiagnostics Diagnostics { get; }

    public GeneratedUnitProcesser(IGeneratedUnitDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedUnitDefinition> Process(IProcessingContext context, RawGeneratedUnitDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        if (definition.Quantity is null)
        {
            return OptionalWithDiagnostics.Empty<GeneratedUnitDefinition>(Diagnostics.NullQuantity(context, definition));
        }

        GeneratedUnitDefinition product = new(definition.Quantity.Value, definition.SupportsBiasedQuantities, definition.GenerateDocumentation, definition.Locations);
        return OptionalWithDiagnostics.Result(product);
    }
}
