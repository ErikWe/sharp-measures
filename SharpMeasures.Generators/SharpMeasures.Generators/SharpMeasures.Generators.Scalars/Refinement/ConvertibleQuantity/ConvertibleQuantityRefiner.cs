namespace SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal interface IConvertibleQuantityRefinementDiagnostics
{
    public abstract Diagnostic? TypeNotScalar(IConvertibleQuantityRefinementContext context, ConvertibleQuantityDefinition definition, int index);
    public abstract Diagnostic? ScalarNotUnbiased(IConvertibleQuantityRefinementContext context, ConvertibleQuantityDefinition definition, int index);
    public abstract Diagnostic? ScalarNotBiased(IConvertibleQuantityRefinementContext context, ConvertibleQuantityDefinition definition, int index);
}

internal interface IConvertibleQuantityRefinementContext : IProcessingContext
{
    public abstract bool Biased { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
}

internal class ConvertibleQuantityRefiner : IProcesser<IConvertibleQuantityRefinementContext, ConvertibleQuantityDefinition,
    RefinedConvertibleQuantityDefinition>
{
    private IConvertibleQuantityRefinementDiagnostics Diagnostics { get; }

    public ConvertibleQuantityRefiner(IConvertibleQuantityRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedConvertibleQuantityDefinition> Process(IConvertibleQuantityRefinementContext context,
        ConvertibleQuantityDefinition definition)
    {
        List<IScalarType> quantities = new(definition.Quantities.Count);
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

            if (context.Biased && scalar.UseUnitBias is false)
            {
                if (Diagnostics.ScalarNotUnbiased(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.Biased is false && scalar.UseUnitBias)
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

        RefinedConvertibleQuantityDefinition product = new(quantities, definition.CastOperatorBehaviour);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
