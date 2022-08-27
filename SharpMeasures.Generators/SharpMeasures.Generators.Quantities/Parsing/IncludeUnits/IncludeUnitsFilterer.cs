namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public interface IIncludeUnitsFilteringDiagnostics
{
    public abstract Diagnostic? UnionInclusionStackingModeRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition);
    public abstract Diagnostic? UnrecognizedUnit(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index);
    public abstract Diagnostic? UnitAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index);
    public abstract Diagnostic? UnitExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index);
}

public interface IIncludeUnitsFilteringContext : IProcessingContext
{
    public abstract IUnitType UnitType { get; }

    public abstract bool AllUnitsIncluded { get; }
    public abstract HashSet<string> IncludedUnits { get; }
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
        List<string> validUnits = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.IncludedUnits.Count; i++)
        {
            var validity = ValidateUnitRecognized(context, definition, i)
                .Validate(() => ValidateUnitNotAlreadyIncluded(context, definition, i))
                .Validate(() => ValidateUnitNotExcluded(context, definition, i));

            allDiagnostics.AddRange(validity);

            if (validity.IsValid)
            {
                validUnits.Add(definition.IncludedUnits[i]);
                locationMap.Add(i);
            }
        }

        var productCreationDelegate = () => new IncludeUnitsDefinition(validUnits, definition.StackingMode, definition.Locations, locationMap);

        return ValidateStackingModeIsNotRedundant(context, definition)
            .Merge(() => OptionalWithDiagnostics.ConditionalWithDefiniteDiagnostics(validUnits.Count is not 0, productCreationDelegate, allDiagnostics));
    }

    private IValidityWithDiagnostics ValidateStackingModeIsNotRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition)
    {
        var stackingModeIsRedundant = definition.Locations.ExplicitlySetStackingMode && definition.StackingMode is InclusionStackingMode.Union && context.AllUnitsIncluded;

        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(stackingModeIsRedundant, () => Diagnostics.UnionInclusionStackingModeRedundant(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitRecognized(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        var recognizedUnit = context.UnitType.UnitsByName.ContainsKey(definition.IncludedUnits[index]);

        return ValidityWithDiagnostics.Conditional(recognizedUnit, () => Diagnostics.UnrecognizedUnit(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitNotAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        var unitAlreadyIncluded = definition.StackingMode is InclusionStackingMode.Union && context.IncludedUnits.Contains(definition.IncludedUnits[index]);

        return ValidityWithDiagnostics.Conditional(unitAlreadyIncluded is false, () => Diagnostics.UnitAlreadyIncluded(context, definition, index));
    }

    private IValidityWithDiagnostics ValidateUnitNotExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index)
    {
        var unitExcluded = definition.StackingMode is InclusionStackingMode.Intersection && context.IncludedUnits.Contains(definition.IncludedUnits[index]) is false;

        return ValidityWithDiagnostics.Conditional(unitExcluded is false, () => Diagnostics.UnitExcluded(context, definition, index));
    }
}
