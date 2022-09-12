namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IIncludeUnitsFilteringDiagnostics
{
    public abstract Diagnostic? UnionInclusionStackingModeRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition);
    public abstract Diagnostic? UnrecognizedUnitInstance(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index);
    public abstract Diagnostic? UnitInstanceAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index);
    public abstract Diagnostic? UnitInstanceExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index);
}

public interface IIncludeUnitsFilteringContext : IProcessingContext
{
    public abstract IUnitType UnitType { get; }

    public abstract bool AllUnitInstancesIncluded { get; }
    public abstract HashSet<string> IncludedUnitInstances { get; }
}

public class IncludeUnitsFilterer : AProcesser<IIncludeUnitsFilteringContext, IncludeUnitsDefinition, IncludeUnitsDefinition>
{
    private IIncludeUnitsFilteringDiagnostics Diagnostics { get; }

    public IncludeUnitsFilterer(IIncludeUnitsFilteringDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<IncludeUnitsDefinition> Process(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition)
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

        var productCreationDelegate = () => new IncludeUnitsDefinition(validUnitInstances, definition.StackingMode, definition.Locations, locationMap);

        return ValidateStackingModeIsNotRedundant(context, definition)
            .Merge(() => OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnitInstances.Count is not 0, productCreationDelegate, allDiagnostics));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsNotRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition)
    {
        var stackingModeIsRedundant = definition.Locations.ExplicitlySetStackingMode && definition.StackingMode is InclusionStackingMode.Union && context.AllUnitInstancesIncluded;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(stackingModeIsRedundant, () => Diagnostics.UnionInclusionStackingModeRedundant(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceRecognized(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        var recognizedUnitInstance = context.UnitType.UnitInstancesByName.ContainsKey(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnitInstance, () => Diagnostics.UnrecognizedUnitInstance(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNotAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        var unitInstanceAlreadyIncluded = definition.StackingMode is InclusionStackingMode.Union && context.IncludedUnitInstances.Contains(definition.UnitInstances[index]);

        return ValidityWithDiagnostics.Conditional(unitInstanceAlreadyIncluded is false, () => Diagnostics.UnitInstanceAlreadyIncluded(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitInstanceNotExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        var unitInstanceExcluded = definition.StackingMode is InclusionStackingMode.Intersection && context.IncludedUnitInstances.Contains(definition.UnitInstances[index]) is false;

        return ValidityWithDiagnostics.Conditional(unitInstanceExcluded is false, () => Diagnostics.UnitInstanceExcluded(context, definition, index));
    }
}
