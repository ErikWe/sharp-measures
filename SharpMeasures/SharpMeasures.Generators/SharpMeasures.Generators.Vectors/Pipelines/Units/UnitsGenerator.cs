namespace SharpMeasures.Generators.Vectors.Pipelines.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Refinement;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Refinement;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> vectorProvider,
        IncrementalValuesProvider<ResizedDataModel> resizedVectorProvider)
    {
        var reduced = vectorProvider.Select(ReduceToDataModel).ReportDiagnostics(context);

        var rootModels = reduced.Collect().Select(ExposeRootDataModels);

        var associatedReduced = resizedVectorProvider.Combine(rootModels).Select(ReduceThroughAssociatedDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(reduced, Execution.Execute);
        context.RegisterSourceOutput(associatedReduced, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> ReduceToDataModel(Vectors.DataModel input, CancellationToken _)
    {
        var processedUnits = GetIncludedUnits(input);

        HashSet<string> includedUnits = new(processedUnits.Result.Select(static (x) => x.Plural));

        VectorConstantRefinementContext vectorConstantContext = new(input.Vector.VectorType, input.Unit, includedUnits);
        VectorConstantRefiner vectorConstantRefiner = new(VectorConstantDiagnostics.Instance);

        var processedConstants = ProcessingFilter.Create(vectorConstantRefiner).Filter(vectorConstantContext, input.Vector.VectorConstants);

        DataModel model = new(input.Vector.VectorType, input.Vector.VectorDefinition.Dimension, input.Unit.UnitType.AsNamedType(), input.Unit.QuantityType,
            processedUnits.Result, processedConstants.Result, input.Documentation);

        var allDiagnostics = processedUnits.Diagnostics.Concat(processedConstants.Diagnostics);

        return ResultWithDiagnostics.Construct(model, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyCollection<UnitInstance>> GetIncludedUnits(Vectors.DataModel input)
    {
        UnitListRefinementContext context = new(input.Vector.VectorType, input.Unit);

        UnitListRefiner<IncludeUnitsDefinition> inclusionRefiner = new(UnitListRefinementDiagnostics<IncludeUnitsDefinition>.Instance);

        if (input.Vector.IncludeUnits.Any())
        {
            var processedInclusions = ProcessingFilter.Create(inclusionRefiner).Filter(context, input.Vector.IncludeUnits, RefinedUnitListDefinition.StartBuilder());

            var includedUnits = processedInclusions.Result.Target.UnitList as IReadOnlyCollection<UnitInstance> ?? Array.Empty<UnitInstance>();
            return ResultWithDiagnostics.Construct(includedUnits, processedInclusions.Diagnostics);
        }

        UnitListRefiner<ExcludeUnitsDefinition> exclusionRefiner = new(UnitListRefinementDiagnostics<ExcludeUnitsDefinition>.Instance);

        var processedExclusions = ProcessingFilter.Create(exclusionRefiner).Filter(context, input.Vector.ExcludeUnits, RefinedUnitListDefinition.StartBuilder());

        List<UnitInstance> allUnits = input.Unit.UnitsByName.Values.ToList();

        foreach (UnitInstance excludedUnit in processedExclusions.Result.Target.UnitList)
        {
            allUnits.Remove(excludedUnit);
        }

        return ResultWithDiagnostics.Construct(allUnits as IReadOnlyCollection<UnitInstance>, processedExclusions.Diagnostics);
    }

    private static ReadOnlyEquatableDictionary<NamedType, DataModel> ExposeRootDataModels(ImmutableArray<DataModel> dataModels, CancellationToken _)
    {
        Dictionary<NamedType, DataModel> dictionary = new(dataModels.Length);

        foreach (DataModel dataModel in dataModels)
        {
            dictionary.Add(dataModel.Vector.AsNamedType(), dataModel);
        }

        return new(dictionary);
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceThroughAssociatedDataModel
        ((ResizedDataModel Model, ReadOnlyEquatableDictionary<NamedType, DataModel> Dictionary) input, CancellationToken _)
    {
        if (input.Dictionary.TryGetValue(input.Model.AssociatedRootVector, out var associatedModel) is false)
        {
            return OptionalWithDiagnostics.Empty<DataModel>();
        }

        HashSet<string> includedUnits = new(associatedModel.Units.Select(static (x) => x.Plural));

        VectorConstantRefinementContext vectorConstantContext = new(input.Model.Vector.VectorType, input.Model.Unit, includedUnits);
        VectorConstantRefiner vectorConstantRefiner = new(VectorConstantDiagnostics.Instance);

        var processedConstants = ProcessingFilter.Create(vectorConstantRefiner).Filter(vectorConstantContext, input.Model.Vector.VectorConstants);

        DataModel model = new(input.Model.Vector.VectorType, input.Model.Vector.VectorDefinition.Dimension, associatedModel.Unit, associatedModel.UnitQuantity,
             associatedModel.Units, processedConstants.Result, input.Model.Documentation);

        return OptionalWithDiagnostics.Result(model, processedConstants.Diagnostics);
    }

    private class UnitListRefinementContext : IUnitListRefinementContext
    {
        public DefinedType Type { get; }

        public UnitInterface Unit { get; }

        public UnitListRefinementContext(DefinedType type, UnitInterface unit)
        {
            Type = type;
            Unit = unit;
        }
    }

    private class VectorConstantRefinementContext : IVectorConstantRefinementContext
    {
        public DefinedType Type { get; }

        public UnitInterface Unit { get; }

        public HashSet<string> IncludedUnits { get; }

        public VectorConstantRefinementContext(DefinedType type, UnitInterface unit, HashSet<string> includedUnits)
        {
            Type = type;
            Unit = unit;
            
            IncludedUnits = includedUnits;
        }
    }
}
