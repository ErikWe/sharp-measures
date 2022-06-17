namespace SharpMeasures.Generators.Units.Refinement.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal interface ISharpMeasuresUnitRefinementDiagnostics
{
    public abstract Diagnostic? QuantityNotScalar(ISharpMeasuresUnitRefinementContext context, SharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityBiased(ISharpMeasuresUnitRefinementContext context, SharpMeasuresUnitDefinition definition);
}

internal interface ISharpMeasuresUnitRefinementContext : IProcessingContext
{
    public abstract ScalarPopulation ScalarPopulation { get; }
}

internal class SharpMeasuresUnitRefiner : IProcesser<ISharpMeasuresUnitRefinementContext, SharpMeasuresUnitDefinition, RefinedSharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitRefinementDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitRefiner(ISharpMeasuresUnitRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedSharpMeasuresUnitDefinition> Process(ISharpMeasuresUnitRefinementContext context, SharpMeasuresUnitDefinition definition)
    {
        if (context.ScalarPopulation.TryGetValue(definition.Quantity, out ScalarInterface quantity) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresUnitDefinition>(Diagnostics.QuantityNotScalar(context, definition));
        }

        if (quantity.UseUnitBias)
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresUnitDefinition>(Diagnostics.QuantityBiased(context, definition));
        }

        RefinedSharpMeasuresUnitDefinition product = new(quantity, definition.BiasTerm);
        return OptionalWithDiagnostics.Result(product);
    }
}
