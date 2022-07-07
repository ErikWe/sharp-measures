namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal interface ISharpMeasuresUnitProcessingDiagnostics
{
    public abstract Diagnostic? NullQuantity(IProcessingContext context, RawSharpMeasuresUnitDefinition definition);
}

internal class SharpMeasuresUnitProcesser : AProcesser<IProcessingContext, RawSharpMeasuresUnitDefinition, UnresolvedSharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitProcesser(ISharpMeasuresUnitProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedSharpMeasuresUnitDefinition> Process(IProcessingContext context, RawSharpMeasuresUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresUnitDefinition>();
        }

        if (definition.Quantity is null)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSharpMeasuresUnitDefinition>(Diagnostics.NullQuantity(context, definition));
        }

        UnresolvedSharpMeasuresUnitDefinition product = new(definition.Quantity.Value, definition.BiasTerm, definition.GenerateDocumentation, definition.Locations);
        return OptionalWithDiagnostics.Result(product);
    }

    private static bool VerifyRequiredPropertiesSet(RawSharpMeasuresUnitDefinition definition)
    {
        return definition.Locations.ExplicitlySetQuantity;
    }
}
