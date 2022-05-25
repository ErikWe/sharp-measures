namespace SharpMeasures.Generators.Scalars.Pipeline.UnitsPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class DiagnosticsStage
{
    public static void ProduceAndReportDiagnostics(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DataModel> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(GatherDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
    }

    private static IReadOnlyList<Diagnostic> GatherDiagnostics(DataModel input, CancellationToken _)
    {
        List<Diagnostic> diagnostics = new();
        ExcessiveAttributes(diagnostics, input);
        UndefinedUnitName(diagnostics, input);
        return diagnostics;
    }

    private static void ExcessiveAttributes(List<Diagnostic> diagnostics, DataModel input)
    {
        if (input.IncludedBases is not null && input.ExcludedBases is not null)
        {
            Diagnostic diagnostic = DiagnosticConstruction.ExcessiveExclusion(input.ExcludedBases.Value.Locations.AttributeName.ToLocation(),
                typeof(IncludeBasesAttribute).Name, typeof(ExcludeBasesAttribute).Name);

            diagnostics.Add(diagnostic);
        }

        if (input.IncludedUnits is not null && input.ExcludedUnits is not null)
        {
            Diagnostic diagnostic = DiagnosticConstruction.ExcessiveExclusion(input.ExcludedUnits.Value.Locations.AttributeName.ToLocation(),
                typeof(IncludeUnitsAttribute).Name, typeof(ExcludeUnitsAttribute).Name);

            diagnostics.Add(diagnostic);
        }
    }

    private static void UndefinedUnitName(List<Diagnostic> diagnostics, DataModel input)
    {
        HashSet<string> existingUnits = new(input.UnitNames.Select(static (x) => x.Name));

        if (input.IncludedBases is not null)
        {
            UndefinedUnitName(diagnostics, input, existingUnits, input.IncludedBases.Value.IncludedBases, input.IncludedBases.Value.Locations.IncludedBasesComponents);
        }

        if (input.ExcludedBases is not null)
        {
            UndefinedUnitName(diagnostics, input, existingUnits, input.ExcludedBases.Value.ExcludedBases, input.ExcludedBases.Value.Locations.ExcludedBasesComponents);
        }

        if (input.IncludedUnits is not null)
        {
            UndefinedUnitName(diagnostics, input, existingUnits, input.IncludedUnits.Value.IncludedUnits, input.IncludedUnits.Value.Locations.IncludedUnitsComponents);
        }

        if (input.ExcludedUnits is not null)
        {
            UndefinedUnitName(diagnostics, input, existingUnits, input.ExcludedUnits.Value.ExcludedUnits, input.ExcludedUnits.Value.Locations.ExcludedUnitsComponents);
        }
    }

    private static void UndefinedUnitName(List<Diagnostic> diagnostics, DataModel input, HashSet<string> existingUnits, IReadOnlyList<string> listedUnits,
        IReadOnlyList<MinimalLocation> locations)
    {
        for (int i = 0; i < listedUnits.Count; i++)
        {
            if (existingUnits.Contains(listedUnits[i]) is false)
            {
                Diagnostic diagnostic = DiagnosticConstruction.UnitNameNotRecognized(locations[i].AsRoslynLocation(), listedUnits[i], input.Definition.Unit.Name);

                diagnostics.Add(diagnostic);
            }
        }
    }
}
