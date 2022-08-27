namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal interface IIncludeBasesFilteringDiagnostics
{
    public abstract Diagnostic? UnionInclusionStackingModeRedundant(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition);
    public abstract Diagnostic? UnrecognizedUnit(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index);
    public abstract Diagnostic? BaseAlreadyIncluded(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index);
    public abstract Diagnostic? BaseExcluded(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index);
}

internal interface IIncludeBasesFilteringContext : IProcessingContext
{
    public abstract IUnitType UnitType { get; }

    public abstract bool AllBasesIncluded { get; }
    public abstract HashSet<string> IncludedBases { get; }
}

internal class IncludeBasesFilterer : AProcesser<IIncludeBasesFilteringContext, IncludeBasesDefinition, IncludeBasesDefinition>
{
    private IIncludeBasesFilteringDiagnostics Diagnostics { get; }

    public IncludeBasesFilterer(IIncludeBasesFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<IncludeBasesDefinition> Process(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition)
    {
        List<string> validUnits = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.IncludedBases.Count; i++)
        {
            var validity = ValidateUnitRecognized(context, definition, i)
                .Validate(() => ValidateBaseNotAlreadyIncluded(context, definition, i))
                .Validate(() => ValidateBaseNotExcluded(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validUnits.Add(definition.IncludedBases[i]);
                locationMap.Add(i);
            }
        }

        var productCreationDelegate = () => new IncludeBasesDefinition(validUnits, definition.StackingMode, definition.Locations, locationMap);

        return ValidateStackingModeIsNotRedundant(context, definition)
            .Merge(() => OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnits.Count is not 0, productCreationDelegate, allDiagnostics));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsNotRedundant(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition)
    {
        var stackingModeIsRedundant = definition.Locations.ExplicitlySetStackingMode && definition.StackingMode is InclusionStackingMode.Union && context.AllBasesIncluded;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(stackingModeIsRedundant, () => Diagnostics.UnionInclusionStackingModeRedundant(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitRecognized(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index)
    {
        var recognizedUnit = context.UnitType.UnitsByName.ContainsKey(definition.IncludedBases[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnit, () => Diagnostics.UnrecognizedUnit(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateBaseNotAlreadyIncluded(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index)
    {
        var baseAlreadyIncluded = definition.StackingMode is InclusionStackingMode.Union && context.IncludedBases.Contains(definition.IncludedBases[index]);

        return ValidityWithDiagnostics.Conditional(baseAlreadyIncluded is false, () => Diagnostics.BaseAlreadyIncluded(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateBaseNotExcluded(IIncludeBasesFilteringContext context, IncludeBasesDefinition definition, int index)
    {
        var baseExcluded = definition.StackingMode is InclusionStackingMode.Intersection && context.IncludedBases.Contains(definition.IncludedBases[index]) is false;

        return ValidityWithDiagnostics.Conditional(baseExcluded is false, () => Diagnostics.BaseExcluded(context, definition, index));
    }
}
