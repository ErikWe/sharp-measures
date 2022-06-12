namespace SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Utility;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IDimensionalEquivalenceProcessingDiagnostics
{
    public abstract Diagnostic? EmptyQuantityList(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition);
    public abstract Diagnostic? NullQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index);
    public abstract Diagnostic? DuplicateQuantity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition, int index);

    public abstract Diagnostic? UnrecognizedCastOperatorBehaviour(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition);
}

public interface IDimensionalEquivalenceProcessingContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedQuantities { get; }
}

public class DimensionalEquivalenceProcesser : AActionableProcesser<IDimensionalEquivalenceProcessingContext, RawDimensionalEquivalenceDefinition,
    DimensionalEquivalenceDefinition>
{
    private IDimensionalEquivalenceProcessingDiagnostics Diagnostics { get; }

    public DimensionalEquivalenceProcesser(IDimensionalEquivalenceProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition,
        DimensionalEquivalenceDefinition product)
    {
        foreach (var quantity in product.Quantities)
        {
            context.ListedQuantities.Add(quantity);
        }
    }

    public override IOptionalWithDiagnostics<DimensionalEquivalenceDefinition> Process(IDimensionalEquivalenceProcessingContext context,
        RawDimensionalEquivalenceDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<DimensionalEquivalenceDefinition>(allDiagnostics);
        }

        var processed = ProcessQuantities(context, definition);
        allDiagnostics = allDiagnostics.Concat(processed);

        if (processed.Result.Quantities.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<DimensionalEquivalenceDefinition>(allDiagnostics);
        }

        return processed.ReplaceDiagnostics(allDiagnostics);
    }

    private IResultWithDiagnostics<DimensionalEquivalenceDefinition> ProcessQuantities(IDimensionalEquivalenceProcessingContext context,
        RawDimensionalEquivalenceDefinition definition)
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

        DimensionalEquivalenceDefinition product = new(quantities.ToList(), definition.CastOperatorBehaviour, definition.Locations);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckValidity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckQuantitiesValidity, CheckCastOperatorBehaviourValidity);
    }

    private IValidityWithDiagnostics CheckQuantitiesValidity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition)
    {
        if (definition.Quantities.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyQuantityList(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckCastOperatorBehaviourValidity(IDimensionalEquivalenceProcessingContext context, RawDimensionalEquivalenceDefinition definition)
    {
        if (Enum.IsDefined(typeof(ConversionOperationBehaviour), definition.CastOperatorBehaviour) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedCastOperatorBehaviour(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
