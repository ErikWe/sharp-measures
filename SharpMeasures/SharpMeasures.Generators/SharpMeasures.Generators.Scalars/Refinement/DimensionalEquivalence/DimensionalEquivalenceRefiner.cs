namespace SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal interface IDimensionalEquivalenceRefinementDiagnostics
{
    public abstract Diagnostic? TypeNotScalar(IDimensionalEquivalenceRefinementContext context, DimensionalEquivalenceDefinition definition, int index);
    public abstract Diagnostic? ScalarNotUnbiased(IDimensionalEquivalenceRefinementContext context, DimensionalEquivalenceDefinition definition, int index);
    public abstract Diagnostic? ScalarNotBiased(IDimensionalEquivalenceRefinementContext context, DimensionalEquivalenceDefinition definition, int index);
}

internal interface IDimensionalEquivalenceRefinementContext : IProcessingContext
{
    public abstract bool Biased { get; }
    public abstract ScalarPopulation ScalarPopulation { get; }
}

internal class DimensionalEquivalenceRefiner : IProcesser<IDimensionalEquivalenceRefinementContext, DimensionalEquivalenceDefinition,
    RefinedDimensionalEquivalenceDefinition>
{
    private IDimensionalEquivalenceRefinementDiagnostics Diagnostics { get; }

    public DimensionalEquivalenceRefiner(IDimensionalEquivalenceRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedDimensionalEquivalenceDefinition> Process(IDimensionalEquivalenceRefinementContext context,
        DimensionalEquivalenceDefinition definition)
    {
        List<ScalarInterface> quantities = new(definition.Quantities.Count);
        List<Diagnostic> allDiagnostics = new();

        var index = 0;
        foreach (var quantity in definition.Quantities)
        {
            if (context.ScalarPopulation.TryGetValue(quantity, out var scalar) is false)
            {
                if (Diagnostics.TypeNotScalar(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.Biased && scalar.Biased is false)
            {
                if (Diagnostics.ScalarNotUnbiased(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.Biased is false && scalar.Biased)
            {
                if (Diagnostics.ScalarNotBiased(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            quantities.Add(scalar);

            index += 1;
        }

        RefinedDimensionalEquivalenceDefinition product = new(quantities, definition.CastOperatorBehaviour);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
