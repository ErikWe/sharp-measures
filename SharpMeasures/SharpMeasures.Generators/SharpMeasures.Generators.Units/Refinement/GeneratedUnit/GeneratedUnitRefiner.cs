namespace SharpMeasures.Generators.Units.Refinement.GeneratedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

internal interface IGeneratedUnitRefinementDiagnostics
{
    public abstract Diagnostic? QuantityNotScalar(IGeneratedUnitRefinementContext context, GeneratedUnitDefinition definition);
    public abstract Diagnostic? QuantityNotUnbiased(IGeneratedUnitRefinementContext context, GeneratedUnitDefinition definition);
}

internal interface IGeneratedUnitRefinementContext : IProcessingContext
{
    public abstract ScalarPopulation ScalarPopulation { get; }
}

internal class GeneratedUnitRefiner : IProcesser<IGeneratedUnitRefinementContext, GeneratedUnitDefinition, RefinedGeneratedUnitDefinition>
{
    private IGeneratedUnitRefinementDiagnostics Diagnostics { get; }

    public GeneratedUnitRefiner(IGeneratedUnitRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedGeneratedUnitDefinition> Process(IGeneratedUnitRefinementContext context, GeneratedUnitDefinition definition)
    {
        if (context.ScalarPopulation.TryGetValue(definition.Quantity, out ScalarInterface quantity) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedUnitDefinition>(Diagnostics.QuantityNotScalar(context, definition));
        }

        if (quantity.Biased)
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedUnitDefinition>(Diagnostics.QuantityNotUnbiased(context, definition));
        }

        RefinedGeneratedUnitDefinition product = new(quantity, definition.SupportsBiasedQuantities);
        return OptionalWithDiagnostics.Result(product);
    }
}
