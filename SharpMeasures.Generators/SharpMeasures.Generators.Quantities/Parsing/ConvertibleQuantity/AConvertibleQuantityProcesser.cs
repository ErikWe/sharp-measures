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

    public abstract Diagnostic? UnrecognizedConversionDirection(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition);
    public abstract Diagnostic? UnrecognizedCastOperatorBehaviour(IConvertibleQuantityProcessingContext context, RawConvertibleQuantityDefinition definition);
}

public interface IConvertibleQuantityProcessingContext : IProcessingContext
{
    public abstract HashSet<NamedType> ListedQuantities { get; }
}

public abstract class AConvertibleQuantityProcesser<TContext, TProduct> : AActionableProcesser<TContext, RawConvertibleQuantityDefinition, TProduct>
    where TContext : IConvertibleQuantityProcessingContext
    where TProduct : IConvertibleQuantity
{
    private IConvertibleQuantityProcessingDiagnostics Diagnostics { get; }

    protected AConvertibleQuantityProcesser(IConvertibleQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, RawConvertibleQuantityDefinition definition, TProduct product)
    {
        foreach (var quantity in product.Quantities)
        {
            context.ListedQuantities.Add(quantity);
        }
    }

    protected IOptionalWithDiagnostics<(IReadOnlyList<NamedType> Quantities, IReadOnlyList<int> LocationMap)> ProcessQuantities(TContext context, RawConvertibleQuantityDefinition definition)
    {
        HashSet<NamedType> quantities = new();
        List<int> locationMap = new(definition.Quantities.Count);

        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Quantities.Count; i++)
        {
            var quantityValidity = ValidateQuantity(context, definition, i);

            if (quantityValidity.IsInvalid)
            {
                allDiagnostics.AddRange(quantityValidity);

                continue;
            }

            if (quantities.Add(definition.Quantities[i]!.Value) is false)
            {
                if (Diagnostics.DuplicateQuantity(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            locationMap.Add(i);
        }

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics<(IReadOnlyList<NamedType>, IReadOnlyList<int>)>(quantities.Count > 0, (quantities.ToList(), locationMap), allDiagnostics);
    }

    protected IValidityWithDiagnostics Validate(TContext context, RawConvertibleQuantityDefinition definition)
    {
        return ValidateNotZeroQuantities(context, definition)
            .Validate(() => ValidateCastOperatorBehaviourDefined(context, definition))
            .Validate(() => ValidateConversionDirectionDefined(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotZeroQuantities(TContext context, RawConvertibleQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Quantities.Count > 0, () => Diagnostics.EmptyQuantityList(context, definition));
    }

    private IValidityWithDiagnostics ValidateCastOperatorBehaviourDefined(TContext context, RawConvertibleQuantityDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(ConversionOperatorBehaviour), definition.CastOperatorBehaviour);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedCastOperatorBehaviour(context, definition));
    }

    private IValidityWithDiagnostics ValidateConversionDirectionDefined(TContext context, RawConvertibleQuantityDefinition definition)
    {
        var enumDefined = Enum.IsDefined(typeof(QuantityConversionDirection), definition.ConversionDirection);

        return ValidityWithDiagnostics.Conditional(enumDefined, () => Diagnostics.UnrecognizedConversionDirection(context, definition));
    }

    protected virtual IValidityWithDiagnostics ValidateQuantity(TContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return ValidateQuantityNotNull(context, definition, index)
            .Validate(() => ValidateQuantityNotSelf(context, definition, index))
            .Validate(() => ValidateQuantityNotDuplicate(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateQuantityNotNull(TContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        return ValidityWithDiagnostics.Conditional(definition.Quantities[index] is not null, () => Diagnostics.NullQuantity(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateQuantityNotSelf(TContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        var quantityIsNotSelf = definition.Quantities[index]!.Value != context.Type.AsNamedType();

        return ValidityWithDiagnostics.Conditional(quantityIsNotSelf, () => Diagnostics.ConvertibleToSelf(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateQuantityNotDuplicate(TContext context, RawConvertibleQuantityDefinition definition, int index)
    {
        var quantityIsDuplicate = context.ListedQuantities.Contains(definition.Quantities[index]!.Value);

        return ValidityWithDiagnostics.Conditional(quantityIsDuplicate is false, () => Diagnostics.DuplicateQuantity(context, definition, index));
    }
}
