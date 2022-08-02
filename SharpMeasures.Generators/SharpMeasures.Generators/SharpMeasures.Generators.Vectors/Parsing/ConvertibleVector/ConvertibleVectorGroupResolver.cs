namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IConvertibleVectorGroupResolutionDiagnostics
{
    public abstract Diagnostic? TypeNotVectorGroup(IConvertibleVectorGroupResolutionContext context, UnresolvedConvertibleVectorDefinition definition, int index);
}

internal interface IConvertibleVectorGroupResolutionContext : IProcessingContext
{
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class ConvertibleVectorGroupResolver : IProcesser<IConvertibleVectorGroupResolutionContext, UnresolvedConvertibleVectorDefinition, ConvertibleVectorDefinition>
{
    private IConvertibleVectorGroupResolutionDiagnostics Diagnostics { get; }

    public ConvertibleVectorGroupResolver(IConvertibleVectorGroupResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleVectorGroupResolutionContext context, UnresolvedConvertibleVectorDefinition definition)
    {
        List<IUnresolvedVectorGroupType> quantities = new(definition.VectorGroups.Count);
        List<Diagnostic> allDiagnostics = new();

        var index = -1;
        foreach (var quantity in definition.VectorGroups)
        {
            index += 1;

            if (context.VectorPopulation.VectorGroups.TryGetValue(quantity, out var vectorGroup))
            {
                quantities.Add(vectorGroup);

                continue;
            }

            if (Diagnostics.TypeNotVectorGroup(context, definition, index) is Diagnostic diagnostics)
            {
                allDiagnostics.Add(diagnostics);
            }
        }

        ConvertibleVectorDefinition product = new(quantities, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
