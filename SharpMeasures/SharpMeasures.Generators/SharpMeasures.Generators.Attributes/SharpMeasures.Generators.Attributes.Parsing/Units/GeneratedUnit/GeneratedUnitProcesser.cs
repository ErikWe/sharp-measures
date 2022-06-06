namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;

public interface IGeneratedUnitDiagnostics
{
    public abstract Diagnostic? NullQuantity(IProcessingContext context, RawGeneratedUnit definition);
}

public class GeneratedUnitProcesser : AProcesser<IProcessingContext, RawGeneratedUnit, GeneratedUnit>
{
    private IGeneratedUnitDiagnostics Diagnostics { get; }

    public GeneratedUnitProcesser(IGeneratedUnitDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<GeneratedUnit> Process(IProcessingContext context, RawGeneratedUnit definition)
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
            return OptionalWithDiagnostics.Empty<GeneratedUnit>(Diagnostics.NullQuantity(context, definition));
        }

        GeneratedUnit product = new(definition.Quantity.Value, definition.SupportsBiasedQuantities, definition.GenerateDocumentation, definition.Locations);
        return OptionalWithDiagnostics.Result(product);
    }
}
