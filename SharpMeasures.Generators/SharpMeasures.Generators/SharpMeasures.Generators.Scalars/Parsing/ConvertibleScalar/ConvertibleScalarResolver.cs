namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;

internal interface IConvertibleScalarResolutionDiagnostics
{
    public abstract Diagnostic? TypeNotScalar(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? ScalarNotUnbiased(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? ScalarNotBiased(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition, int index);
}

internal interface IConvertibleScalarResolutionContext : IProcessingContext
{
    public abstract bool UseUnitBias { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
}

internal class ConvertibleScalarResolver : IProcesser<IConvertibleScalarResolutionContext, UnresolvedConvertibleScalarDefinition, ConvertibleScalarDefinition>
{
    private IConvertibleScalarResolutionDiagnostics Diagnostics { get; }

    public ConvertibleScalarResolver(IConvertibleScalarResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleScalarDefinition> Process(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition)
    {
        List<IUnresolvedScalarType> quantities = new(definition.Scalars.Count);
        List<Diagnostic> allDiagnostics = new();

        var index = 0;
        foreach (var quantity in definition.Scalars)
        {
            if (context.ScalarPopulation.BaseScalarByScalarType.TryGetValue(quantity, out var baseScalar) is false)
            {
                if (Diagnostics.TypeNotScalar(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.UseUnitBias && baseScalar.Definition.UseUnitBias is false)
            {
                if (Diagnostics.ScalarNotUnbiased(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.UseUnitBias is false && baseScalar.Definition.UseUnitBias)
            {
                if (Diagnostics.ScalarNotBiased(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.ScalarPopulation.Scalars.TryGetValue(quantity, out var scalar) is false)
            {
                continue;
            }

            quantities.Add(scalar);

            index += 1;
        }

        ConvertibleScalarDefinition product = new(quantities, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
