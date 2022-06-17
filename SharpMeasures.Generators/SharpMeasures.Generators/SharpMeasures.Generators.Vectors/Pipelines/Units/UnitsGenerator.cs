﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Refinement;
using SharpMeasures.Generators.Vectors.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Vectors.DataModel> generatedVectorProvider,
        IncrementalValuesProvider<ResizedDataModel> resizedVectorProvider, IncrementalValueProvider<UnitInclusionPopulation> unitInclusionProvider)
    {
        var reducedGeneratedVectors = generatedVectorProvider.Select(ReduceToDataModel).ReportDiagnostics(context);

        var rootModels = reducedGeneratedVectors.Collect().Select(ExposeRootDataModels);

        var reducedResizedVectors = resizedVectorProvider.Combine(rootModels, unitInclusionProvider).Select(ReduceThroughRootDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(reducedGeneratedVectors, Execution.Execute);
        context.RegisterSourceOutput(reducedResizedVectors, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> ReduceToDataModel(Vectors.DataModel input, CancellationToken _)
    {
        var processedUnits = GetIncludedUnits(input);

        HashSet<string> includedUnits = new(processedUnits.Result.Select(static (x) => x.Plural));

        VectorConstantRefinementContext vectorConstantContext = new(input.VectorData.VectorType, input.VectorDefinition.Unit, includedUnits);
        VectorConstantRefiner vectorConstantRefiner = new(VectorConstantDiagnostics.Instance);

        var processedConstants = ProcessingFilter.Create(vectorConstantRefiner).Filter(vectorConstantContext, input.VectorData.VectorConstants);

        DataModel model = new(input.VectorData.VectorType, input.VectorDefinition.Dimension, input.VectorDefinition.Scalar?.ScalarType.AsNamedType(),
            input.VectorDefinition.Unit, input.VectorDefinition.Unit.QuantityType, processedUnits.Result, processedConstants.Result, input.Documentation);

        var allDiagnostics = processedUnits.Diagnostics.Concat(processedConstants.Diagnostics);

        return ResultWithDiagnostics.Construct(model, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyCollection<UnitInstance>> GetIncludedUnits(Vectors.DataModel input)
    {
        UnitListRefinementContext context = new(input.VectorData.VectorType, input.VectorDefinition.Unit);

        UnitListRefiner<IncludeUnitsDefinition> inclusionRefiner = new(UnitListRefinementDiagnostics<IncludeUnitsDefinition>.Instance);

        if (input.VectorData.IncludeUnits.Any())
        {
            var processedInclusions = ProcessingFilter.Create(inclusionRefiner).Filter(context, input.VectorData.IncludeUnits, RefinedUnitListDefinition.StartBuilder());

            var includedUnits = processedInclusions.Result.Target.UnitList as IReadOnlyCollection<UnitInstance> ?? Array.Empty<UnitInstance>();
            return ResultWithDiagnostics.Construct(includedUnits, processedInclusions.Diagnostics);
        }

        UnitListRefiner<ExcludeUnitsDefinition> exclusionRefiner = new(UnitListRefinementDiagnostics<ExcludeUnitsDefinition>.Instance);

        var processedExclusions = ProcessingFilter.Create(exclusionRefiner).Filter(context, input.VectorData.ExcludeUnits, RefinedUnitListDefinition.StartBuilder());

        List<UnitInstance> allUnits = input.VectorDefinition.Unit.UnitsByName.Values.ToList();

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

        return dictionary.AsReadOnlyEquatable();
    }

    private static IOptionalWithDiagnostics<DataModel> ReduceThroughRootDataModel
        ((ResizedDataModel Model, ReadOnlyEquatableDictionary<NamedType, DataModel> RootModels, UnitInclusionPopulation UnitInclusionPopulation) input,
        CancellationToken _)
    {
        if (input.RootModels.TryGetValue(input.Model.VectorDefinition.AssociatedVector.VectorType, out var associatedModel) is false)
        {
            return OptionalWithDiagnostics.Empty<DataModel>();
        }

        AssociatedUnitInclusionRefinementContext unitInclusionContext = new(input.Model.VectorData.VectorType, input.Model.VectorData.VectorDefinition.AssociatedVector,
            associatedModel.Unit, input.UnitInclusionPopulation);

        AssociatedUnitInclusionRefiner<ParsedResizedVector, IncludeUnitsDefinition, ExcludeUnitsDefinition> unitInclusionRefiner
            = new(AssociatedUnitInclusionRefinementDiagnostics<IncludeUnitsDefinition, ExcludeUnitsDefinition>.Instance);

        var processedUnitInclusion = unitInclusionRefiner.Process(unitInclusionContext, input.Model.VectorData);

        HashSet<string> includedUnitsHashSet = new(processedUnitInclusion.Result.UnitList.Select(static (x) => x.Plural));

        VectorConstantRefinementContext vectorConstantContext = new(input.Model.VectorData.VectorType, associatedModel.Unit, includedUnitsHashSet);
        VectorConstantRefiner vectorConstantRefiner = new(VectorConstantDiagnostics.Instance);

        var processedConstants = ProcessingFilter.Create(vectorConstantRefiner).Filter(vectorConstantContext, input.Model.VectorData.VectorConstants);

        DataModel model = new(input.Model.VectorData.VectorType, input.Model.VectorDefinition.Dimension, associatedModel.Scalar, associatedModel.Unit,
            associatedModel.UnitQuantity, associatedModel.Units, processedConstants.Result, input.Model.Documentation);

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

    private class AssociatedUnitInclusionRefinementContext : IAssociatedUnitInclusionRefinementContext
    {
        public DefinedType Type { get; }
        public NamedType AssociatedType { get; }
        public UnitInterface Unit { get; }

        public UnitInclusionPopulation UnitInclusionPopulation { get; }

        public AssociatedUnitInclusionRefinementContext(DefinedType type, NamedType associatedType, UnitInterface unit, UnitInclusionPopulation unitInclusionPopulation)
        {
            Type = type;
            AssociatedType = associatedType;
            Unit = unit;

            UnitInclusionPopulation = unitInclusionPopulation;
        }
    }
}