namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Scalars;

using System.Collections.Generic;

internal interface IConvertibleScalarPostResolutionFilterDiagnostics
{
    public abstract Diagnostic? DuplicateScalar(IConvertibleScalarPostResolutionFilterContext context, ConvertibleScalarDefinition definition, int index);
}

internal interface IConvertibleScalarPostResolutionFilterContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedScalars { get; }
}

internal class ConvertibleScalarPostResolutionFilter : AProcesser<IConvertibleScalarPostResolutionFilterContext, ConvertibleScalarDefinition, ConvertibleScalarDefinition>
{
    private IConvertibleScalarPostResolutionFilterDiagnostics Diagnostics { get; }

    public ConvertibleScalarPostResolutionFilter(IConvertibleScalarPostResolutionFilterDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ConvertibleScalarDefinition> Process(IConvertibleScalarPostResolutionFilterContext context, ConvertibleScalarDefinition definition)
    {
        var filteredScalars = FilterScalars(context, definition);
        var allDiagnostics = filteredScalars.Diagnostics;

        ConvertibleScalarDefinition product = new(filteredScalars.Result, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IResultWithDiagnostics<IReadOnlyList<IRawScalarType>> FilterScalars(IConvertibleScalarPostResolutionFilterContext context, ConvertibleScalarDefinition definition)
    {
        List<IRawScalarType> scalars = new(definition.Scalars.Count);
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Scalars.Count; i++)
        {
            if (context.ListedScalars.Contains(definition.Scalars[i].Type.AsNamedType()))
            {
                if (Diagnostics.DuplicateScalar(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            scalars.Add(definition.Scalars[i]);
            context.ListedScalars.Add(definition.Scalars[i].Type.AsNamedType());
        }

        return ResultWithDiagnostics.Construct(scalars as IReadOnlyList<IRawScalarType>, allDiagnostics);
    }
}
