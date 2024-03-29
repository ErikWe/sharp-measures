﻿namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IConvertibleScalarFilteringDiagnostics
{
    public abstract Diagnostic? TypeNotScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? ScalarNotUnbiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? ScalarNotBiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
}

internal interface IConvertibleScalarFilteringContext : IProcessingContext
{
    public abstract bool UseUnitBias { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
}

internal sealed class ConvertibleScalarFilterer : IProcesser<IConvertibleScalarFilteringContext, ConvertibleScalarDefinition, ConvertibleScalarDefinition>
{
    private IConvertibleScalarFilteringDiagnostics Diagnostics { get; }

    public ConvertibleScalarFilterer(IConvertibleScalarFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleScalarDefinition> Process(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition)
    {
        List<NamedType> scalars = new(definition.Quantities.Count);
        List<int> locationMap = new(definition.Quantities.Count);

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.Quantities.Count; i++)
        {
            var validity = ValidateScalar(context, definition, i);

            if (validity.IsValid)
            {
                scalars.Add(definition.Quantities[i]);
                locationMap.Add(i);
            }

            allDiagnostics.AddRange(validity);
        }

        var productDelegate = () => new ConvertibleScalarDefinition(scalars, definition.ConversionDirection, definition.CastOperatorBehaviour, definition.Locations, locationMap);

        return OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(scalars.Count > 0, productDelegate, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        return ResolveScalarBase(context, definition, index)
            .Validate((scalarBase) => ValidateScalarNotUnexpectedlyBiased(context, definition, index, scalarBase))
            .Validate((scalarBase) => ValidateScalarNotUnexpectedlyUnbiased(context, definition, index, scalarBase))
            .Reduce();
    }

    private IOptionalWithDiagnostics<IScalarBaseType> ResolveScalarBase(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        var correctlyResolvedScalar = context.ScalarPopulation.ScalarBases.TryGetValue(definition.Quantities[index], out var scalarBase);

        return OptionalWithDiagnostics.Conditional(correctlyResolvedScalar, scalarBase, () => Diagnostics.TypeNotScalar(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateScalarNotUnexpectedlyBiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index, IScalarBaseType scalarBase)
    {
        var scalarUnexpectedlyBiased = context.UseUnitBias is false && scalarBase.Definition.UseUnitBias;

        return ValidityWithDiagnostics.Conditional(scalarUnexpectedlyBiased is false, () => Diagnostics.ScalarNotUnbiased(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateScalarNotUnexpectedlyUnbiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index, IScalarBaseType scalarBase)
    {
        var scalarUnexpectedlyUnbiased = context.UseUnitBias && scalarBase.Definition.UseUnitBias is false;

        return ValidityWithDiagnostics.Conditional(scalarUnexpectedlyUnbiased is false, () => Diagnostics.ScalarNotBiased(context, definition, index));
    }
}
