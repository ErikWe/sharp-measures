namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IConvertibleVectorResolutionDiagnostics
{
    public abstract Diagnostic? TypeNotVector(IConvertibleVectorResolutionContext context, UnresolvedConvertibleVectorDefinition definition, int index);
}

internal interface IConvertibleVectorResolutionContext : IProcessingContext
{
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class ConvertibleVectorResolver : IProcesser<IConvertibleVectorResolutionContext, UnresolvedConvertibleVectorDefinition, ConvertibleVectorDefinition>
{
    private IConvertibleVectorResolutionDiagnostics Diagnostics { get; }

    public ConvertibleVectorResolver(IConvertibleVectorResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleVectorResolutionContext context, UnresolvedConvertibleVectorDefinition definition)
    {
        List<IUnresolvedIndividualVectorType> quantities = new(definition.VectorGroups.Count);
        List<Diagnostic> allDiagnostics = new();

        var index = 0;
        foreach (var quantity in definition.VectorGroups)
        {
            if (context.VectorPopulation.IndividualVectors.TryGetValue(quantity, out var baseScalar) is false)
            {
                if (Diagnostics.TypeNotVector(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.VectorPopulation.IndividualVectors.TryGetValue(quantity, out var scalar) is false)
            {
                continue;
            }

            quantities.Add(scalar);

            index += 1;
        }

        ConvertibleVectorDefinition product = new(quantities, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
