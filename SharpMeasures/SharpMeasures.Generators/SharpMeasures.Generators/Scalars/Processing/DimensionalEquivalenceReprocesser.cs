namespace SharpMeasures.Generators.Scalars.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Scalars.Processing.Diagnostics;

using System;
using System.Collections.Generic;

internal class DimensionalEquivalenceProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public ScalarPopulation ScalarPopulation { get; }

    public DimensionalEquivalenceProcessingContext(DefinedType type, ScalarPopulation scalarPopulation)
    {
        Type = type;

        ScalarPopulation = scalarPopulation;
    }
}

internal class DimensionalEquivalenceReprocesser : IReprocesser<DimensionalEquivalenceProcessingContext, DimensionalEquivalence,
    ProcessedDimensionalEquivalence>
{
    public static DimensionalEquivalenceReprocesser Instance { get; } = new();

    private DimensionalEquivalenceReprocesser() { }

    public IOptionalWithDiagnostics<ProcessedDimensionalEquivalence> Reprocess(DimensionalEquivalenceProcessingContext context, DimensionalEquivalence definition,
        ProcessedDimensionalEquivalence product)
    {
        List<Diagnostic> allDiagnostics = new();

        Action<ScalarInterface> addOperation = definition.CastOperatorBehaviour switch
        {
            ConversionOperationBehaviour.None => product.EquivalentQuantitiesWithoutCast.Add,
            ConversionOperationBehaviour.Explicit => product.EquivalentQuantitiesWithExplicitCast.Add,
            ConversionOperationBehaviour.Implicit => product.EquivalentQuantitiesWithImplicitCast.Add,
            _ => product.EquivalentQuantitiesWithoutCast.Add
        };

        if (context.ScalarPopulation.TryGetValue(context.Type.AsNamedType(), out ScalarInterface equivalentTo) is false)
        {
            return OptionalWithDiagnostics.Empty<ProcessedDimensionalEquivalence>();
        }

        for (int i = 0; i < definition.Quantities.Count; i++)
        {
            if (context.ScalarPopulation.TryGetValue(definition.Quantities[i], out ScalarInterface quantity) is false)
            {
                allDiagnostics.Add(DimensionalEquivalenceDiagnostics.TypeNotScalar(definition, i));
                continue;
            }

            if (equivalentTo.Biased && quantity.Biased is false)
            {
                allDiagnostics.Add(DimensionalEquivalenceDiagnostics.QuantityNotBiased(definition, i));
                continue;
            }

            if (equivalentTo.Biased is false && quantity.Biased)
            {
                allDiagnostics.Add(DimensionalEquivalenceDiagnostics.QuantityNotUnbiased(definition, i));
                continue;
            }

            addOperation(quantity);
        }

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
