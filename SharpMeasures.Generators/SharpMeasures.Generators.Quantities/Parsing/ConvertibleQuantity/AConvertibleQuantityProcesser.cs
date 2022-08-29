namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Utility;

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
    where TProduct : IConvertibleQuantity
{
    private IConvertibleQuantityProcessingDiagnostics Diagnostics { get; }

    protected AConvertibleQuantityProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition, TProduct product)
    {
        foreach (var quantity in product.Quantities)
        {
            context.ListedQuantities.Add(quantity);
        }
    }

    protected IOptionalWithDiagnostics<(IReadOnlyList<NamedType> Quantities, IReadOnlyList<int> LocationMap)> ProcessQuantities(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        HashSet<NamedType> quantities = new();
        List<int> locationMap = new(definition.Quantities.Count);

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

            locationMap.Add(i);
        }

        return OptionalWithDiagnostics.Conditional<(IReadOnlyList<NamedType>, IReadOnlyList<int>)>(quantities.Count > 0, (quantities.ToList(), locationMap), allDiagnostics);
    }

    protected IValidityWithDiagnostics Validate(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return ValidateNotZeroQuantities(context, definition)
            .Validate(() => ValidateCastOperatorBehaviourDefined(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotZeroQuantities(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Quantities.Count > 0, () => Diagnostics.EmptyQuantityList(context, definition));
    }

    private IValidityWithDiagnostics ValidateCastOperatorBehaviourDefined(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.CastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedCastOperatorBehaviour(context, definition));
    }
}
