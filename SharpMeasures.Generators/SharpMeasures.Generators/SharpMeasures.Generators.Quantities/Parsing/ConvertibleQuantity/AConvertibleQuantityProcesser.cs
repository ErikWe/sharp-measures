namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Utility;
using SharpMeasures.Generators.Unresolved.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IConvertibleQuantityProcessingDiagnostics
{
    public abstract Diagnostic? EmptyQuantityList(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition);
    public abstract Diagnostic? NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);
    public abstract Diagnostic? DuplicateQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);
    public abstract Diagnostic? ConvertibleToSelf(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition);
}

public interface IConvertibleQuantityProcessingContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedQuantities { get; }
}

public abstract class AConvertibleQuantityProcesser<TProduct> : AActionableProcesser<IConvertibleQuantityProcessingContext, RawConvertibleQuantityDefinition, TProduct>
    where TProduct : IUnresolvedConvertibleQuantity
{
    private IConvertibleQuantityProcessingDiagnostics Diagnostics { get; }

    protected AConvertibleQuantityProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition,
        TProduct product)
    {
        foreach (var quantity in product.Quantities)
        {
            context.ListedQuantities.Add(quantity);
        }
    }

    protected IResultWithDiagnostics<IReadOnlyList<NamedType>> ProcessQuantities(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        HashSet<NamedType> quantities = new();
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Quantities.Count; i++)
        {
            if (definition.Quantities[i] is not NamedType candidate)
            {
                if (Diagnostics.NullQuantity(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (candidate == context.Type.AsNamedType())
            {
                if (Diagnostics.ConvertibleToSelf(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (context.ListedQuantities.Contains(candidate) || quantities.Add(candidate) is false)
            {
                if (Diagnostics.DuplicateQuantity(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }
        }

        return ResultWithDiagnostics.Construct<IReadOnlyList<NamedType>>(quantities.ToList(), allDiagnostics);
    }

    protected IValidityWithDiagnostics CheckValidity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckQuantitiesValidity, CheckCastOperatorBehaviourValidity);
    }

    private IValidityWithDiagnostics CheckQuantitiesValidity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        if (definition.Quantities.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyQuantityList(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckCastOperatorBehaviourValidity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        if (Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.CastOperatorBehaviour) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedCastOperatorBehaviour(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
