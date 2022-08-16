namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Scalars;

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
    public abstract IRawScalarPopulation ScalarPopulation { get; }
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
        List<IRawScalarType> quantities = new(definition.Scalars.Count);
        List<Diagnostic> allDiagnostics = new();

        var index = 0;
        foreach (var quantity in definition.Scalars)
        {
            if (context.ScalarPopulation.ScalarBases.TryGetValue(quantity, out var quantityBase) is false)
            {
                if (Diagnostics.TypeNotScalar(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.UseUnitBias && quantityBase.Definition.UseUnitBias is false)
            {
                if (Diagnostics.ScalarNotBiased(context, definition, index) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.UseUnitBias is false && quantityBase.Definition.UseUnitBias)
            {
                if (Diagnostics.ScalarNotUnbiased(context, definition, index) is Diagnostic diagnostics)
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
