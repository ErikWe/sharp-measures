namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Utility;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IDimensionalEquivalenceDiagnostics
{
    public abstract Diagnostic? EmptyQuantityList(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition);
    public abstract Diagnostic? NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition, int index);
    public abstract Diagnostic? DuplicateQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition, int index);

    public abstract Diagnostic? UnrecognizedCastOperatorBehaviour(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition);
}

public interface IDimensionalEquivalenceProcessingContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedQuantities { get; }
}

public class DimensionalEquivalenceProcesser : AActionableProcesser<IDimensionalEquivalenceProcessingContext, RawDimensionalEquivalence,
    DimensionalEquivalence>
{
    private IDimensionalEquivalenceDiagnostics Diagnostics { get; }

    public DimensionalEquivalenceProcesser(IDimensionalEquivalenceDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition,
        DimensionalEquivalence product)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        foreach (var quantity in product.Quantities)
        {
            context.ListedQuantities.Add(quantity);
        }
    }

    public override IOptionalWithDiagnostics<DimensionalEquivalence> Process(IDimensionalEquivalenceProcessingContext context,
        RawDimensionalEquivalence definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DimensionalEquivalence>(allDiagnostics);
        }

        var processed = ProcessQuantities(context, definition);
        allDiagnostics = allDiagnostics.Concat(processed);

        if (processed.Result.Quantities.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<DimensionalEquivalence>(allDiagnostics);
        }

        return processed.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<DimensionalEquivalence> ProcessQuantities(IDimensionalEquivalenceProcessingContext context,
        RawDimensionalEquivalence definition)
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

        DimensionalEquivalence product = new(quantities.ToList(), definition.CastOperatorBehaviour, definition.Locations);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckValidity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckQuantitiesValidity, CheckCastOperatorBehaviourValidity);
    }

    private IValidityWithDiagnostics CheckQuantitiesValidity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition)
    {
        if (definition.Quantities.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyQuantityList(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckCastOperatorBehaviourValidity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalence definition)
    {
        if (Enum.IsDefined(typeof(ConversionOperationBehaviour), definition.CastOperatorBehaviour) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedCastOperatorBehaviour(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
