namespace SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using System.Collections.Generic;

internal interface IConvertibleVectorPostResolutionFilterDiagnostics
{
    public abstract Diagnostic? DuplicateVector(IConvertibleVectorPostResolutionFilterContext context, ConvertibleVectorDefinition definition, int index);
}

internal interface IConvertibleVectorPostResolutionFilterContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedVectors { get; }
}

internal class ConvertibleVectorPostResolutionFilter : AProcesser<IConvertibleVectorPostResolutionFilterContext, ConvertibleVectorDefinition, ConvertibleVectorDefinition>
{
    private IConvertibleVectorPostResolutionFilterDiagnostics Diagnostics { get; }

    public ConvertibleVectorPostResolutionFilter(IConvertibleVectorPostResolutionFilterDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ConvertibleVectorDefinition> Process(IConvertibleVectorPostResolutionFilterContext context, ConvertibleVectorDefinition definition)
    {
        var filteredVectors = FilterVectors(context, definition);
        var allDiagnostics = filteredVectors.Diagnostics;

        ConvertibleVectorDefinition product = new(filteredVectors.Result, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IResultWithDiagnostics<IReadOnlyList<IRawVectorGroupType>> FilterVectors(IConvertibleVectorPostResolutionFilterContext context, ConvertibleVectorDefinition definition)
    {
        List<IRawVectorGroupType> vectors = new(definition.VectorGroups.Count);
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.VectorGroups.Count; i++)
        {
            if (context.ListedVectors.Contains(definition.VectorGroups[i].Type.AsNamedType()))
            {
                if (Diagnostics.DuplicateVector(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            vectors.Add(definition.VectorGroups[i]);
            context.ListedVectors.Add(definition.VectorGroups[i].Type.AsNamedType());
        }

        return ResultWithDiagnostics.Construct(vectors as IReadOnlyList<IRawVectorGroupType>, allDiagnostics);
    }
}
