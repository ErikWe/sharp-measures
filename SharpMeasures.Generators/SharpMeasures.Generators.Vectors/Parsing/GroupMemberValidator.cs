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
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupMemberValidator
{
    public static IncrementalValuesProvider<Optional<GroupMemberType>> Validate(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<GroupMemberType>> vectorProvider,
       IncrementalValueProvider<VectorProcessingData> processingDataProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider,
       IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        return vectorProvider.Combine(processingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(Validate).ReportDiagnostics(context);
    }

    private static IOptionalWithDiagnostics<GroupMemberType> Validate((Optional<GroupMemberType> UnvalidatedVector, VectorProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupMemberType>();
        }

        return Validate(input.UnvalidatedVector.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    private static IOptionalWithDiagnostics<GroupMemberType> Validate(GroupMemberType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var vector = ValidateVector(vectorType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<GroupMemberType>();
        }

        var unit = unitPopulation.Units[vectorPopulation.GroupBases[vectorType.Definition.VectorGroup].Definition.Unit];

        var inheritedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnitsFromMembers,
            static (vector) => vector.Definition.InheritUnits, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);

        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = ValidateIncludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(vectorType, unit, inheritedUnitInstanceNames);

        var definedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => false,
            static (vector) => false, static (vector) => false);

        var allUnits = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var derivations = ValidateDerivations(vectorType, scalarPopulation, vectorPopulation);
        var constants = ValidateConstants(vectorType, vectorPopulation, unit, allUnits);
        var conversions = ValidateConversions(vectorType, vectorPopulation);

        GroupMemberType product = new(vectorType.Type, vectorType.TypeLocation, vector.Result, derivations.Result, constants.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);

        var allDiagnostics = vector.Concat(derivations).Concat(constants).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ValidateVector(GroupMemberType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorGroupMemberValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(SharpMeasuresVectorGroupMemberValidator).Validate(validationContext, vectorType.Definition).Transform(vectorType.Definition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ValidateDerivations(GroupMemberType vectorType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(vectorType.Type, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(DerivedQuantityValidator).Filter(validationContext, vectorType.Derivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ValidateConstants(GroupMemberType vectorType, IVectorPopulation vectorPopulation, IUnitType unit, IEnumerable<IUnitInstance> includedUnits)
    {
        var inheritedConstants = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => Array.Empty<IVectorConstant>(),
            static (vector) => vector.Definition.InheritConstantsFromMembers, static (vector) => false, static (vector) => false);

        HashSet<string> inheritedConstantNames = new(inheritedConstants.Select(static (constant) => constant.Name));
        HashSet<string> inheritedConstantMultiples = new(inheritedConstants.Where(static (constant) => constant.GenerateMultiplesProperty).Select(static (constant) => constant.Multiples!));

        HashSet<string> incluedUnitPlurals = new(includedUnits.Select(static (unit) => unit.PluralForm));

        var validationContext = new VectorConstantValidationContext(vectorType.Type, vectorType.Definition.Dimension, unit, inheritedConstantNames, inheritedConstantMultiples, incluedUnitPlurals);

        return ProcessingFilter.Create(VectorConstantValidator).Filter(validationContext, vectorType.Constants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ValidateConversions(GroupMemberType vectorType, IVectorPopulation vectorPopulation)
    {
        var inheritedConversions = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Conversions.SelectMany(static (vectorList) => vectorList.Quantities),
            static (vector) => vector.Conversions.SelectMany(static (vectorList) => vectorList.Quantities), static (vector) => vector.Definition.InheritConversionsFromMembers,
            static (vector) => vector.Definition.InheritConversions, static (vector) => vector.Definition.InheritConversions);

        var filteringContext = new ConvertibleVectorFilteringContext(vectorType.Type, vectorType.Definition.Dimension, VectorType.GroupMember, vectorPopulation, new HashSet<NamedType>(inheritedConversions));

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, vectorType.Conversions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ValidateIncludeUnitInstances(GroupMemberType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceInclusions);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ValidateExcludeUnitInstances(GroupMemberType vectorType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(vectorType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, vectorType.UnitInstanceExclusions);
    }

    private static IEnumerable<T> CollectInheritedItems<T>(GroupMemberType vectorType, IVectorPopulation vectorPopulation, Func<IVectorGroupMemberType, IEnumerable<T>> memberItemsDelegate, Func<IVectorGroupType, IEnumerable<T>> groupItemsDelegate,
        Func<IVectorGroupMemberType, bool> shouldMemberInheritFromMembers, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromGroup, Func<IVectorGroupSpecializationType, bool> shouldGroupInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Groups[vectorType.Definition.VectorGroup], null, shouldMemberInheritFromMembers(vectorType), shouldMemberInheritFromGroup(vectorType));

        return items;

        void recursivelyAddItems(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool shouldInheritFromMember, bool shouldInheritFromGroup)
        {
            if (shouldInheritFromGroup)
            {
                items.AddRange(groupItemsDelegate(vectorGroup));
            }

            if (shouldInheritFromMember && correspondingMember is not null)
            {
                items.AddRange(memberItemsDelegate(correspondingMember));
            }

            recurse(vectorGroup, correspondingMember, shouldInheritFromMember, shouldInheritFromGroup);
        }

        void recurse(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool inheritedFromMember, bool inheritedFromGroup)
        {
            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                var originalVectorGroup = vectorPopulation.Groups[vectorGroupSpecialization.Definition.OriginalQuantity];

                vectorPopulation.GroupMembersByGroup[originalVectorGroup.Type.AsNamedType()].GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember);

                bool shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                bool shouldInheritFromGroup = originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyAddItems(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(GroupMemberType vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit,
        Func<IVectorGroupMemberType, bool> shouldMemberInheritFromMembers, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromGroup, Func<IVectorGroupSpecializationType, bool> shouldGroupInherit,
        bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recursivelyModify(vectorPopulation.Groups[vectorType.Definition.VectorGroup], onlyInherited ? null : vectorType, shouldMemberInheritFromGroup(vectorType), onlyInherited);

        return includedUnits;

        void recursivelyModify(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool shouldInheritFromMember, bool shouldInheritFromGroup)
        {
            if (shouldInheritFromGroup)
            {
                modify(vectorGroup.UnitInstanceInclusions, vectorGroup.UnitInstanceExclusions);
            }

            if (shouldInheritFromMember && correspondingMember is not null)
            {
                modify(correspondingMember.UnitInstanceInclusions, correspondingMember.UnitInstanceExclusions);
            }

            recurse(vectorGroup, correspondingMember, shouldInheritFromMember, shouldInheritFromGroup);
        }

        void modify(IReadOnlyList<IUnitInstanceList> inclusions, IReadOnlyList<IUnitInstanceList> exclusions)
        {
            if (inclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(inclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(exclusions));

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

        void recurse(IVectorGroupType vectorGroup, IVectorGroupMemberType? correspondingMember, bool inheritedFromMember, bool inheritedFromGroup)
        {
            if (vectorGroup is IVectorGroupSpecializationType vectorGroupSpecialization)
            {
                var originalVectorGroup = vectorPopulation.Groups[vectorGroupSpecialization.Definition.OriginalQuantity];

                vectorPopulation.GroupMembersByGroup[originalVectorGroup.Type.AsNamedType()].GroupMembersByDimension.TryGetValue(vectorType.Definition.Dimension, out var originalCorrespondingMember);

                bool shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                bool shouldInheritFromGroup = originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyModify(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private static SharpMeasuresVectorGroupMemberValidator SharpMeasuresVectorGroupMemberValidator { get; } = new(SharpMeasuresVectorGroupMemberValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(DerivedQuantityValidationDiagnostics.Instance);
    private static VectorConstantValidator VectorConstantValidator { get; } = new(VectorConstantValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(ConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(IncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(ExcludeUnitsFilteringDiagnostics.Instance);
}
