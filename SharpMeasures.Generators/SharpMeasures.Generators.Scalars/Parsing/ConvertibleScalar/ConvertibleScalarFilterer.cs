namespace SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IConvertibleScalarFilteringDiagnostics
{
    public abstract Diagnostic? TypeNotScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? ScalarNotUnbiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? ScalarNotBiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
    public abstract Diagnostic? DuplicateScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index);
}

internal interface IConvertibleScalarFilteringContext : IProcessingContext
{
    public abstract bool UseUnitBias { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }

    public abstract HashSet<NamedType> InheritedConversions { get; }
}

internal class ConvertibleScalarFilterer : IProcesser<IConvertibleScalarFilteringContext, ConvertibleScalarDefinition, ConvertibleScalarDefinition>
{
    private IConvertibleScalarFilteringDiagnostics Diagnostics { get; }

    public ConvertibleScalarFilterer(IConvertibleScalarFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<ConvertibleScalarDefinition> Process(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition)
    {
        List<NamedType> scalars = new(definition.Scalars.Count);
        List<int> locationMap = new(definition.Scalars.Count);

        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Scalars.Count; i++)
        {
            var validity = ValidateScalar(context, definition, i);

            if (validity.IsValid)
            {
                scalars.Add(definition.Scalars[i]);
                locationMap.Add(i);
            }

            allDiagnostics.AddRange(validity);
        }

        ConvertibleScalarDefinition product = new(scalars, definition.Bidirectional, definition.CastOperatorBehaviour, definition.Locations, locationMap);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        return ValidateScalarNotDuplicateWithInherited(context, definition, index)
            .Merge(() => ResolveScalarBase(context, definition, index))
            .Validate((scalarBase) => ValidateScalarNotUnexpectedlyBiased(context, definition, index, scalarBase))
            .Validate((scalarBase) => ValidateScalarNotUnexpectedlyUnbiased(context, definition, index, scalarBase))
            .Reduce();
    }

    private IValidityWithDiagnostics ValidateScalarNotDuplicateWithInherited(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        var scalarIsDuplicate = context.InheritedConversions.Contains(definition.Scalars[index]);

        return ValidityWithDiagnostics.Conditional(scalarIsDuplicate is false, () => Diagnostics.DuplicateScalar(context, definition, index));
    }

    private IOptionalWithDiagnostics<IScalarBaseType> ResolveScalarBase(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        var scalarIsScalar = context.ScalarPopulation.ScalarBases.TryGetValue(definition.Scalars[index], out var scalarBase);

        return OptionalWithDiagnostics.Conditional(scalarIsScalar, scalarBase, () => Diagnostics.TypeNotScalar(context, definition, index));
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
