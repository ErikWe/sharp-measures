namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal interface IIncludeUnitBasesFilteringDiagnostics
{
    public abstract Diagnostic? UnionInclusionStackingModeRedundant(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition);
    public abstract Diagnostic? UnrecognizedUnitInstance(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index);
    public abstract Diagnostic? UnitInstanceAlreadyIncluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index);
    public abstract Diagnostic? UnitInstanceExcluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index);
}

internal interface IIncludeUnitBasesFilteringContext : IProcessingContext
{
    public abstract IUnitType UnitType { get; }

    public abstract bool AllAllUnitInstancesIncluded { get; }
    public abstract HashSet<string> IncludedUnitInstanceNames { get; }
}

internal sealed class IncludeUnitBasesFilterer : AProcesser<IIncludeUnitBasesFilteringContext, IncludeUnitBasesDefinition, IncludeUnitBasesDefinition>
{
    private IIncludeUnitBasesFilteringDiagnostics Diagnostics { get; }

    public IncludeUnitBasesFilterer(IIncludeUnitBasesFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<IncludeUnitBasesDefinition> Process(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition)
    {
        List<string> validUnitInstances = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.UnitInstances.Count; i++)
        {
            var validity = ValidateUnitInstanceRecognized(context, definition, i)
                .Validate(() => ValidateUnitInstanceNotAlreadyIncluded(context, definition, i))
                .Validate(() => ValidateUnitInstanceNotExcluded(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validUnitInstances.Add(definition.UnitInstances[i]);
                locationMap.Add(i);
            }
        }

        var productCreationDelegate = () => new IncludeUnitBasesDefinition(validUnitInstances, definition.StackingMode, definition.Locations, locationMap);

        return ValidateStackingModeIsNotRedundant(context, definition)
            .Merge(() => OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnitInstances.Count > 0, productCreationDelegate, allDiagnostics));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsNotRedundant(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition)
    {
        var stackingModeIsRedundant = definition.Locations.ExplicitlySetStackingMode && definition.StackingMode is InclusionStackingMode.Union && context.AllAllUnitInstancesIncluded;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(stackingModeIsRedundant, () => Diagnostics.UnionInclusionStackingModeRedundant(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceRecognized(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index)
    {
        var recognizedUnitInstance = context.UnitType.UnitInstancesByName.ContainsKey(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnitInstance, () => Diagnostics.UnrecognizedUnitInstance(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNotAlreadyIncluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index)
    {
        var unitInstanceAlreadyIncluded = definition.StackingMode is InclusionStackingMode.Union && context.IncludedUnitInstanceNames.Contains(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(unitInstanceAlreadyIncluded is false, () => Diagnostics.UnitInstanceAlreadyIncluded(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNotExcluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index)
    {
        var unitInstanceExcluded = definition.StackingMode is InclusionStackingMode.Intersection && context.IncludedUnitInstanceNames.Contains(definition.UnitInstances[index]) is false;

        return ValidityWithDiagnostics.Conditional(unitInstanceExcluded is false, () => Diagnostics.UnitInstanceExcluded(context, definition, index));
    }
}
