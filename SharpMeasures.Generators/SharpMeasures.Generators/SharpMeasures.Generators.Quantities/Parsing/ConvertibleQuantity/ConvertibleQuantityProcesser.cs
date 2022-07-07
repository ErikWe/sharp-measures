namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IConvertibleQuantityProcessingDiagnostics
{
    public abstract Diagnostic? EmptyQuantityList(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition);
    public abstract Diagnostic? NullQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);
    public abstract Diagnostic? DuplicateQuantity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition);
}

public interface IConvertibleQuantityProcessingContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedQuantities { get; }
}

public class ConvertibleQuantityProcesser : AActionableProcesser<IConvertibleQuantityProcessingContext, RawConvertibleQuantityDefinition, ConvertibleQuantityDefinition>
{
    private IConvertibleQuantityProcessingDiagnostics Diagnostics { get; }

    public ConvertibleQuantityProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition,
        ConvertibleQuantityDefinition product)
    {
        foreach (var quantity in product.Quantities)
        {
            context.ListedQuantities.Add(quantity);
        }
    }

    public override IOptionalWithDiagnostics<ConvertibleQuantityDefinition> Process(IConvertibleQuantityProcessingContext context,
        RawConvertibleQuantityDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ConvertibleQuantityDefinition>(allDiagnostics);
        }

        var processed = ProcessQuantities(context, definition);
        allDiagnostics = allDiagnostics.Concat(processed);

        if (processed.Result.Quantities.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<ConvertibleQuantityDefinition>(allDiagnostics);
        }

        return processed.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<ConvertibleQuantityDefinition> ProcessQuantities(IConvertibleQuantityProcessingContext context,
        RawConvertibleQuantityDefinition definition)
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

            if (context.ListedQuantities.Contains(candidate) || quantities.Add(candidate) is false)
            {
                if (Diagnostics.DuplicateQuantity(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }
        }

        ConvertibleQuantityDefinition product = new(quantities.ToList(), definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckValidity(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
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
