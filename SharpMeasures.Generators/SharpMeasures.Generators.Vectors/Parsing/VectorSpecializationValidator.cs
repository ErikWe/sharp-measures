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
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
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
    public static IncrementalValuesProvider<VectorSpecializationType> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorSpecializationType> vectorProvider,
       IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<VectorSpecializationType> Validate((VectorSpecializationType UnvalidatedVector, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken _)
    {
        var vector = ValidateVector(input.UnvalidatedVector, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<VectorSpecializationType>();
        }

        var vectorBase = input.VectorPopulation.VectorBases[input.UnvalidatedVector.Type.AsNamedType()];

        var unit = input.UnitPopulation.Units[vectorBase.Definition.Unit];

        var inheritedUnits = GetUnitInclusions(input.UnvalidatedVector, input.VectorPopulation, unit.UnitsByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitNames = new HashSet<string>(inheritedUnits.Select(static (unit) => unit.Name));

        var unitInclusions = ValidateIncludeUnits(input.UnvalidatedVector, unit, inheritedUnitNames);
        var unitExclusions = ValidateExcludeUnits(input.UnvalidatedVector, unit, inheritedUnitNames);

        var definedUnits = GetUnitInclusions(input.UnvalidatedVector, input.VectorPopulation, inheritedUnits, unit, static (vector) => false);

        var allUnits = inheritedUnits.Concat(definedUnits).ToList();

        var derivations = ValidateDerivations(input.UnvalidatedVector, input.ScalarPopulation, input.VectorPopulation);
        var constants = ValidateConstants(input.UnvalidatedVector, input.VectorPopulation, vectorBase.Definition.Dimension, unit, allUnits);
        var conversions = ValidateConversions(input.UnvalidatedVector, input.VectorPopulation, vectorBase.Definition.Dimension);

        VectorSpecializationType product = new(input.UnvalidatedVector.Type, input.UnvalidatedVector.TypeLocation, vector.Result, derivations.Result, constants.Result, conversions.Result,
            unitInclusions.Result, unitExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitInclusions).Concat(unitExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> ValidateVector(VectorSpecializationType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresVectorValidationContext(vectorType.Type, unitPopulation, scalarPopulation, vectorPopulation);

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

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select(static (unit) => unit.Plural));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, dimension, unit, inheritedConstantNames, inheritedConstantMultiples, incluedUnitPlurals);

        return ValidityFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, int dimension)
    {
        var inheritedConversions = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Conversions.SelectMany(static (vectorList) => vectorList.Vectors), static (vector) => vector.Definition.InheritConversions);

        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, dimension, VectorType.Vector, vectorPopulation, new HashSet<NamedType>(inheritedConversions));

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnits(VectorSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnits(VectorSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitExclusions);
    }

    private static IEnumerable<T> CollectInheritedItems<T>(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorType, IEnumerable<T>> itemsDelegate, Func<IVectorSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Vectors[vectorType.Definition.OriginalVector]);

        return items;

        void recursivelyAddItems(IVectorType vector)
        {
            items.AddRange(itemsDelegate(vector));

            if (vector is IVectorSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalVector]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInclusions(VectorSpecializationType vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit,
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
            if (vector.UnitInclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(vector.UnitInclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(vector.UnitExclusions));

            IEnumerable<IUnitInstance> listUnits(IEnumerable<IUnitList> unitLists)
            {
                foreach (var unitName in unitLists.SelectMany(static (unitList) => unitList.Units))
                {
                    if (unit.UnitsByName.TryGetValue(unitName, out var unitInstance))
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
                recurisvelyAdd(vectorPopulation.Vectors[vectorSpecialization.Definition.OriginalVector]);
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
