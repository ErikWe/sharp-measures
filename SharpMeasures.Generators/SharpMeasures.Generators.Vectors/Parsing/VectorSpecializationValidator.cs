namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class VectorSpecializationValidator
{
    public static IncrementalValuesProvider<Optional<VectorSpecializationType>> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorSpecializationType>> vectorProvider,
       IncrementalValueProvider<VectorProcessingData> processingDataProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return vectorProvider.Combine(processingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<VectorSpecializationType> Validate((Optional<VectorSpecializationType> UnvalidatedVector, VectorProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<VectorSpecializationType>();
        }

        return Validate(input.UnvalidatedVector.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IOptionalWithDiagnostics<VectorSpecializationType> Validate(VectorSpecializationType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var vector = ValidateVector(vectorType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<VectorSpecializationType>();
        }

        var vectorBase = vectorPopulation.VectorBases[vectorType.Type.AsNamedType()];

        var unit = unitPopulation.Units[vectorBase.Definition.Unit];

        var inheritedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = ValidateIncludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);

        var definedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, inheritedUnitInstances, unit, static (vector) => false);

        var allUnitInstances = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var derivations = ValidateDerivations(vectorType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(vectorType, vectorPopulation, vectorBase.Definition.Dimension, unit, allUnitInstances);
        var conversions = ValidateConversions(vectorType, vectorPopulation, vectorBase.Definition.Dimension);

        VectorSpecializationType product = new(vectorType.Type, vectorType.TypeLocation, vector.Result, derivations.Result, constants.Result, conversions.Result,
            unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> ValidateVector(VectorSpecializationType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresVectorValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorValidator).Filter(validationContext, vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(VectorSpecializationType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ValidateConstants(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, int dimension, IUnitType unit, IEnumerable<IUnitInstance> includedUnits)
    {
        var inheritedConstants = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => vector.Definition.InheritConstants);

        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select(static (unit) => unit.PluralForm));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, dimension, unit, inheritedConstantNames, inheritedConstantMultiples, incluedUnitPlurals);

        return ProcessingFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, int dimension)
    {
        var inheritedConversions = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Conversions.SelectMany(static (vectorList) => vectorList.Quantities), static (vector) => vector.Definition.InheritConversions);

        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, dimension, VectorType.Vector, vectorPopulation, new HashSet<NamedType>(inheritedConversions));

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(VectorSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(VectorSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceExclusions);
    }

    private static IEnumerable<T> CollectInheritedItems<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Vectors[vectorType.Definition.OriginalQuantity]);

        return items;

        void recursivelyAddItems(IVectorType vector)
        {
            items.AddRange(itemsDelegate(vector));

            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit,
        Func<IVectorSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyAdd(vectorType, onlyInherited);

        return includedUnits;

        void recurisvelyAdd(IVectorType vector, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                modify(vector);
            }

            recurse(vector);
        }

        void modify(IVectorType vector)
        {
            if (vector.UnitInstanceInclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(vector.UnitInstanceInclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(vector.UnitInstanceExclusions));

            IEnumerable<IUnitInstance> listUnits(IEnumerable<IUnitInstanceList> unitLists)
            {
                foreach (var unitName in unitLists.SelectMany(static (unitList) => unitList.UnitInstances))
                {
                    if (unit.UnitInstancesByName.TryGetValue(unitName, out var unitInstance))
                    {
                        yield return unitInstance;
                    }
                }
            }
        }

        void recurse(IVectorType vector)
        {
            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recurisvelyAdd(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static SpecializedSharpMeasuresVectorValidator SpecializedSharpMeasuresVectorValidator { get; } = new(SpecializedSharpMeasuresVectorValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static VectorConstantValidator VectorConstantValidator { get; } = new(VectorConstantValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
