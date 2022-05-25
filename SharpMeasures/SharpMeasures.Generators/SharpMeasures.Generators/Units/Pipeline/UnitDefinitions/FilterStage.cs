namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class FilterStage
{
    public static IncrementalValuesProvider<DataModel> FilterInvalidDefinitions(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DataModel> inputProvider)
    {
        var withoutDuplicates = inputProvider.Select(FilterDuplicateNamesAndDependencyMissing);

        context.ReportDiagnostics(withoutDuplicates);
        return withoutDuplicates.ExtractResult();
    }

    private static ResultWithDiagnostics<DataModel> FilterDuplicateNamesAndDependencyMissing(DataModel input, CancellationToken _)
    {
        HashSet<string> definedNames = new();
        List<Diagnostic> diagnostics = new();

        var filteredUnitAliases = FilterDuplicates(input, input.UnitAliases, definedNames, diagnostics);
        var filteredDerivedUnits = FilterDuplicates(input, input.DerivedUnits, definedNames, diagnostics);
        var filteredFixedUnits = FilterDuplicates(input, input.FixedUnits, definedNames, diagnostics);
        var filteredOffsetUnits = FilterDuplicates(input, input.OffsetUnits, definedNames, diagnostics);
        var filteredPrefixedUnits = FilterDuplicates(input, input.PrefixedUnits, definedNames, diagnostics);
        var filteredScaledUnits = FilterDuplicates(input, input.ScaledUnits, definedNames, diagnostics);

        filteredUnitAliases = FilterMissingDependencies(input, filteredUnitAliases, definedNames, diagnostics);
        filteredOffsetUnits = FilterMissingDependencies(input, filteredOffsetUnits, definedNames, diagnostics);
        filteredPrefixedUnits = FilterMissingDependencies(input, filteredPrefixedUnits, definedNames, diagnostics);
        filteredScaledUnits = FilterMissingDependencies(input, filteredScaledUnits, definedNames, diagnostics);

        DataModel result = input with
        {
            UnitAliases = filteredUnitAliases,
            DerivedUnits = filteredDerivedUnits,
            FixedUnits = filteredFixedUnits,
            OffsetUnits = filteredOffsetUnits,
            PrefixedUnits = filteredPrefixedUnits,
            ScaledUnits = filteredScaledUnits
        };

        return new(result, diagnostics);
    }

    private static List<T> FilterDuplicates<T>(DataModel input, IReadOnlyList<T> definitions, HashSet<string> definedNames, List<Diagnostic> diagnostics)
        where T : ICacheableUnitDefinition
    {
        List<T> filteredDefinitions = new(definitions.Count);

        foreach (T definition in definitions)
        {
            if (definedNames.Contains(definition.Name))
            {
                diagnostics.Add(CreateDuplicateUnitNameDiagnostics(input, definition));
                continue;
            }

            definedNames.Add(definition.Name);
            filteredDefinitions.Add(definition);
        }

        return filteredDefinitions;
    }

    private static List<T> FilterMissingDependencies<T>(DataModel input, IReadOnlyList<T> definitions, HashSet<string> unitNames, List<Diagnostic> diagnostics)
        where T : ICacheableDependantUnitDefinition
    {
        List<T> filteredDefinitions = new(definitions.Count);

        foreach (T definition in definitions)
        {
            if (unitNames.Contains(definition.DependantOn) is false)
            {
                diagnostics.Add(CreateUnitNameNotRecognizedDiagnostics(input, definition));
                continue;
            }

            filteredDefinitions.Add(definition);
        }

        return filteredDefinitions;
    }

    private static Diagnostic CreateDuplicateUnitNameDiagnostics<T>(DataModel input, T definition)
        where T : ICacheableUnitDefinition
    {
        return DiagnosticConstruction.DuplicateUnitName(definition.Locations.Name.ToLocation(), definition.Name, input.Unit.Name);
    }

    private static Diagnostic CreateUnitNameNotRecognizedDiagnostics<T>(DataModel input, T definition)
        where T : ICacheableDependantUnitDefinition
    {
        return DiagnosticConstruction.UnitNameNotRecognized(definition.Locations.DependantOn.ToLocation(), definition.DependantOn, input.Unit.Name);
    }
}
