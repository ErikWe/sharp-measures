namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Processing;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> ReduceToDataModel(Scalars.DataModel input, CancellationToken _)
    {
        var processedBases = GetIncludedBases(input);
        var processedUnits = GetIncludedUnits(input);

        HashSet<string> unitNames = new(processedBases.Result.Select(static (x) => x.Name));
        HashSet<string> unitPlurals = new(processedUnits.Result.Select(static (x) => x.Plural));

        Processing.ScalarConstantProcessingContext scalarConstantContext = new(input.Scalar.ScalarType, input.Unit, unitNames, unitPlurals);
        var processedConstants = ProcessingFilter.Create(Processing.ScalarConstantProcesser.Instance).Filter(scalarConstantContext,
            input.Scalar.ScalarConstants);

        DataModel model = new(input.Scalar.ScalarType, input.Unit.UnitType.AsNamedType(), input.Unit.QuantityType, processedBases.Result, processedUnits.Result,
            processedConstants.Result, input.Documentation);

        var allDiagnostics = processedBases.Diagnostics.Concat(processedUnits.Diagnostics).Concat(processedConstants.Diagnostics);

        return ResultWithDiagnostics.Construct(model, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyCollection<UnitInstance>> GetIncludedBases(Scalars.DataModel input)
    {
        UnitListProcessingContext context = new(input.Scalar.ScalarType, input.Unit);

        if (input.Scalar.IncludeBases.Any())
        {
            var processedIncludeBases = ProcessingFilter.Create(UnitListProcesser<IncludeBases>.Instance).Filter(context,
                input.Scalar.IncludeBases, new ProcessedUnitList());

            var includedBases = processedIncludeBases.Result.UnitList as IReadOnlyCollection<UnitInstance> ?? Array.Empty<UnitInstance>();
            return ResultWithDiagnostics.Construct(includedBases, processedIncludeBases.Diagnostics);
        }

        var processedExcludBases = ProcessingFilter.Create(UnitListProcesser<ExcludeBases>.Instance).Filter(context,
            input.Scalar.ExcludeBases, new ProcessedUnitList());

        List<UnitInstance> bases = input.Unit.UnitsByName.Values.ToList();

        foreach (UnitInstance excludedBase in processedExcludBases.Result.UnitList)
        {
            bases.Remove(excludedBase);
        }

        return ResultWithDiagnostics.Construct(bases as IReadOnlyCollection<UnitInstance>, processedExcludBases.Diagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyCollection<UnitInstance>> GetIncludedUnits(Scalars.DataModel input)
    {
        UnitListProcessingContext context = new(input.Scalar.ScalarType, input.Unit);

        if (input.Scalar.IncludeUnits.Any())
        {
            var processedIncludeUnits = ProcessingFilter.Create(UnitListProcesser<IncludeUnits>.Instance).Filter(context,
                input.Scalar.IncludeUnits, new ProcessedUnitList());

            var includedUnits = processedIncludeUnits.Result.UnitList as IReadOnlyCollection<UnitInstance> ?? Array.Empty<UnitInstance>();
            return ResultWithDiagnostics.Construct(includedUnits, processedIncludeUnits.Diagnostics);
        }

        var processedExcludUnits = ProcessingFilter.Create(UnitListProcesser<ExcludeUnits>.Instance).Filter(context,
            input.Scalar.ExcludeUnits, new ProcessedUnitList());

        List<UnitInstance> units = input.Unit.UnitsByName.Values.ToList();

        foreach (UnitInstance excludedUnit in processedExcludUnits.Result.UnitList)
        {
            units.Remove(excludedUnit);
        }

        return ResultWithDiagnostics.Construct(units as IReadOnlyCollection<UnitInstance>, processedExcludUnits.Diagnostics);
    }
}
