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
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupSpecializationValidator
{
    public static IncrementalValuesProvider<Optional<GroupSpecializationType>> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<GroupSpecializationType>> vectorProvider,
       IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider)
    {
        return vectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<GroupSpecializationType> Validate((Optional<GroupSpecializationType> UnvalidatedVector, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulationWithData VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupSpecializationType>();
        }

        return Validate(input.UnvalidatedVector.Value, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IOptionalWithDiagnostics<GroupSpecializationType> Validate(GroupSpecializationType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var vector = ValidateVector(vectorType, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<GroupSpecializationType>();
        }

        var groupBase = vectorPopulation.GroupBases[vectorType.Type.AsNamedType()];

        var unit = unitPopulation.Units[groupBase.Definition.Unit];

        var inheritedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = ValidateIncludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);

        var derivations = ValidateDerivations(vectorType, scalarPopulation, vectorPopulation);
        var conversions = ValidateConversions(vectorType, vectorPopulation);

        GroupSpecializationType product = new(vectorType.Type, vectorType.TypeLocation, vector.Result, derivations.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> ValidateVector(GroupSpecializationType vectorType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulationWithData vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresVectorGroupValidationContext(vectorType.Type, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupValidator).Filter(validationContext, vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(GroupSpecializationType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation)
    {
        var inheritedConversions = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Conversions.SelectMany(static (vectorList) => vectorList.Quantities), static (vector) => vector.Definition.InheritConversions);

        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, VectorType.Group, vectorPopulation, new HashSet<NamedType>(inheritedConversions));

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(GroupSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(GroupSpecializationType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceExclusions);
    }

    private static IEnumerable<T> CollectInheritedItems<T>(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, IEnumerable<T>> itemsDelegate, Func<IVectorGroupSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Groups[vectorType.Definition.OriginalQuantity]);

        return items;

        void recursivelyAddItems(IVectorGroupType vector)
        {
            items.AddRange(itemsDelegate(vector));

            if (vector is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(GroupSpecializationType vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit,
        Func<IVectorGroupSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyAdd(vectorType, onlyInherited);

        return includedUnits;

        void recurisvelyAdd(IVectorGroupType vector, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                modify(vector);
            }

            recurse(vector);
        }

        void modify(IVectorGroupType vector)
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

        void recurse(IVectorGroupType vector)
        {
            if (vector is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recurisvelyAdd(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static SpecializedSharpMeasuresVectorGroupValidator SpecializedSharpMeasuresVectorGroupValidator { get; } = new(SpecializedSharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
