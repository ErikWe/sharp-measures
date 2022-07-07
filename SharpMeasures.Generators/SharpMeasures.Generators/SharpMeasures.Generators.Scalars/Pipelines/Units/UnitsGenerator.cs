namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Refinement.UnitList;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;
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
        var processedBases = GetIncludedUnits(input, input.ScalarData.includeBases, input.ScalarData.excludeBases);
        var processedUnits = GetIncludedUnits(input, input.ScalarData.includeUnits, input.ScalarData.excludeUnits);

        HashSet<string> includedBases = new(processedBases.Result.Select(static (x) => x.Name));
        HashSet<string> includedUnits = new(processedUnits.Result.Select(static (x) => x.Plural));

        ScalarConstantRefinementContext scalarConstantContext = new(input.ScalarData.ScalarType, input.ScalarDefinition.Unit, includedBases, includedUnits);
        ScalarConstantRefiner scalarConstantRefiner = new(ScalarConstantDiagnostics.Instance);

        var processedConstants = ProcessingFilter.Create(scalarConstantRefiner).Filter(scalarConstantContext, input.ScalarData.constants);

        DataModel model = new(input.ScalarData.ScalarType, input.ScalarDefinition.Unit.Type.AsNamedType(), input.ScalarDefinition.Unit.QuantityType,
            processedBases.Result, processedUnits.Result, processedConstants.Result, input.Documentation);

        var allDiagnostics = processedBases.Diagnostics.Concat(processedUnits.Diagnostics).Concat(processedConstants.Diagnostics);

        return ResultWithDiagnostics.Construct(model, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyCollection<UnitInstance>> GetIncludedUnits<TInclusion, TExclusion>(Scalars.DataModel input,
        IEnumerable<TInclusion> inclusions, IEnumerable<TExclusion> exclusions)
        where TInclusion : IOpenItemListDefinition<string>
        where TExclusion : IOpenItemListDefinition<string>
    {
        UnitListRefinementContext context = new(input.ScalarData.ScalarType, input.ScalarDefinition.Unit);

        UnitListRefiner<TInclusion> inclusionRefiner = new(UnitListRefinementDiagnostics<TInclusion>.Instance);

        if (inclusions.Any())
        {
            var processedInclusions = ProcessingFilter.Create(inclusionRefiner).Filter(context, inclusions, RefinedUnitListDefinition.StartBuilder());

            var includedUnits = processedInclusions.Result.Target.UnitList as IReadOnlyCollection<UnitInstance> ?? Array.Empty<UnitInstance>();
            return ResultWithDiagnostics.Construct(includedUnits, processedInclusions.Diagnostics);
        }

        UnitListRefiner<TExclusion> exclusionRefiner = new(UnitListRefinementDiagnostics<TExclusion>.Instance);

        var processedExclusions = ProcessingFilter.Create(exclusionRefiner).Filter(context, exclusions, RefinedUnitListDefinition.StartBuilder());

        List<UnitInstance> allUnits = input.ScalarDefinition.Unit.UnitsByName.Values.ToList();

        foreach (UnitInstance excludedUnit in processedExclusions.Result.Target.UnitList)
        {
            allUnits.Remove(excludedUnit);
        }

        return ResultWithDiagnostics.Construct(allUnits as IReadOnlyCollection<UnitInstance>, processedExclusions.Diagnostics);
    }

    private class UnitListRefinementContext : IUnitListRefinementContext
    {
        public DefinedType Type { get; }

        public IUnitType Unit { get; }

        public UnitListRefinementContext(DefinedType type, IUnitType unit)
        {
            Type = type;
            Unit = unit;
        }
    }

    private class ScalarConstantRefinementContext : IScalarConstantRefinementContext
    {
        public DefinedType Type { get; }

        public IUnitType Unit { get; }

        public HashSet<string> IncludedBases { get; }
        public HashSet<string> IncludedUnits { get; }

        public ScalarConstantRefinementContext(DefinedType type, IUnitType unit, HashSet<string> includedBases, HashSet<string> includedUnits)
        {
            Type = type;
            Unit = unit;
            
            IncludedBases = includedBases;
            IncludedUnits = includedUnits;
        }
    }
}
