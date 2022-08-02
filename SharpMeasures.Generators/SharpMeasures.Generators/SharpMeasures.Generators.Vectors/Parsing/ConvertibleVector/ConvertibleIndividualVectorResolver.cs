namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IConvertibleIndividualVectorResolutionDiagnostics
{
    public abstract Diagnostic? TypeNotVector(IConvertibleIndividualVectorResolutionContext context, UnresolvedConvertibleVectorDefinition definition, int index);
}

internal interface IConvertibleIndividualVectorResolutionContext : IProcessingContext
{
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class ConvertibleIndividualVectorResolver : IProcesser<IConvertibleIndividualVectorResolutionContext, UnresolvedConvertibleVectorDefinition, ConvertibleVectorDefinition>
{
    private IConvertibleIndividualVectorResolutionDiagnostics Diagnostics { get; }

    public ConvertibleIndividualVectorResolver(IConvertibleIndividualVectorResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleIndividualVectorResolutionContext context, UnresolvedConvertibleVectorDefinition definition)
    {
        List<IUnresolvedVectorGroupType> quantities = new(definition.VectorGroups.Count);
        List<Diagnostic> allDiagnostics = new();

        var index = -1;
        foreach (var quantity in definition.VectorGroups)
        {
            index += 1;

            if (context.VectorPopulation.IndividualVectors.TryGetValue(quantity, out var individualVector))
            {
                quantities.Add(individualVector);

                continue;
            }

            if (context.VectorPopulation.VectorGroups.TryGetValue(quantity, out var vectorGroup))
            {
                quantities.Add(vectorGroup);

                continue;
            }

            if (Diagnostics.TypeNotVector(context, definition, index) is Diagnostic diagnostics)
            {
                allDiagnostics.Add(diagnostics);
            }
        }

        ConvertibleVectorDefinition product = new(quantities, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
