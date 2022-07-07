namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;

internal interface ISharpMeasuresUnitResolutionDiagnostics
{
    public abstract Diagnostic? QuantityNotScalar(ISharpMeasuresUnitResolutionContext context, UnresolvedSharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityBiased(ISharpMeasuresUnitResolutionContext context, UnresolvedSharpMeasuresUnitDefinition definition);
}

internal interface ISharpMeasuresUnitResolutionContext : IProcessingContext
{
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
}

internal class SharpMeasuresUnitResolver : AProcesser<ISharpMeasuresUnitResolutionContext, UnresolvedSharpMeasuresUnitDefinition, SharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitResolver(ISharpMeasuresUnitResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> Process(ISharpMeasuresUnitResolutionContext context, UnresolvedSharpMeasuresUnitDefinition definition)
    {
        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Quantity, out var quantity) is false)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresUnitDefinition>(Diagnostics.QuantityNotScalar(context, definition));
        }

        if (quantity.ScalarDefinition.UseUnitBias)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresUnitDefinition>(Diagnostics.QuantityBiased(context, definition));
        }

        SharpMeasuresUnitDefinition product = new(quantity, definition.BiasTerm, definition.GenerateDocumentation, definition.Locations);
        return OptionalWithDiagnostics.Result(product);
    }
}
