namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using System.Linq;

internal static class ForeignGroupSpecializationValidator
{
    public static Optional<GroupSpecializationType> Validate(GroupSpecializationType groupType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var group = ValidateGroup(groupType, unitPopulation, scalarPopulation, vectorPopulation);

        if (group.HasValue is false)
        {
            return new Optional<GroupSpecializationType>();
        }

        var groupBase = vectorPopulation.GroupBases[groupType.Type.AsNamedType()];

        var unit = unitPopulation.Units[groupBase.Definition.Unit];

        var inheritedUnitInstances = GetUnitInstanceInclusions(groupType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);
        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = ValidateIncludeUnitInstances(groupType, unit, inheritedUnitInstanceNames);
        var unitInstanceExclusions = ValidateExcludeUnitInstances(groupType, unit, inheritedUnitInstanceNames);

        var derivations = ValidateDerivations(groupType, scalarPopulation, vectorPopulation);
        var conversions = ValidateConversions(groupType, vectorPopulation);

        return new GroupSpecializationType(groupType.Type, groupType.TypeLocation, group.Value, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    private static Optional<SpecializedSharpMeasuresVectorGroupDefinition> ValidateGroup(GroupSpecializationType groupType, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        VectorProcessingData processingData = new(new Dictionary<NamedType, IVectorBaseType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorSpecializationType>(), new Dictionary<NamedType, IVectorGroupBaseType>(),
            new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupSpecializationType>(), new Dictionary<NamedType, IVectorGroupMemberType>());

        var validationContext = new SpecializedSharpMeasuresVectorGroupValidationContext(groupType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        var group = ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupValidator).Filter(validationContext, groupType.Definition);

        if (group.LacksResult)
        {
            return new Optional<SpecializedSharpMeasuresVectorGroupDefinition>();
        }

        return group.Result;
    }

    private static IReadOnlyList<DerivedQuantityDefinition> ValidateDerivations(GroupSpecializationType groupType, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new DerivedQuantityValidationContext(groupType.Type, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(DerivedQuantityValidator).Filter(validationContext, groupType.Derivations).Result;
    }

    private static IReadOnlyList<ConvertibleVectorDefinition> ValidateConversions(GroupSpecializationType groupType, IVectorPopulation vectorPopulation)
    {
        var inheritedConversions = CollectInheritedItems(groupType, vectorPopulation, static (vector) => vector.Conversions.SelectMany(static (vectorList) => vectorList.Quantities), static (vector) => vector.Definition.InheritConversions);

        var filteringContext = new ConvertibleVectorFilteringContext(groupType.Type, VectorType.Group, vectorPopulation, new HashSet<NamedType>(inheritedConversions));

        return ProcessingFilter.Create(ConvertibleVectorFilterer).Filter(filteringContext, groupType.Conversions).Result;
    }

    private static IReadOnlyList<IncludeUnitsDefinition> ValidateIncludeUnitInstances(GroupSpecializationType groupType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new IncludeUnitsFilteringContext(groupType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(IncludeUnitsFilterer).Filter(filteringContext, groupType.UnitInstanceInclusions).Result;
    }

    private static IReadOnlyList<ExcludeUnitsDefinition> ValidateExcludeUnitInstances(GroupSpecializationType groupType, IUnitType unit, HashSet<string> inheritedUnits)
    {
        var filteringContext = new ExcludeUnitsFilteringContext(groupType.Type, unit, inheritedUnits);

        return ProcessingFilter.Create(ExcludeUnitsFilterer).Filter(filteringContext, groupType.UnitInstanceExclusions).Result;
    }

    private static IEnumerable<T> CollectInheritedItems<T>(GroupSpecializationType groupType, IVectorPopulation vectorPopulation, Func<IVectorGroupType, IEnumerable<T>> itemsDelegate, Func<IVectorGroupSpecializationType, bool> shouldInherit)
    {
        List<T> items = new();

        recursivelyAddItems(vectorPopulation.Groups[groupType.Definition.OriginalQuantity]);

        return items;

        void recursivelyAddItems(IVectorGroupType group)
        {
            items.AddRange(itemsDelegate(group));

            if (group is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recursivelyAddItems(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(GroupSpecializationType groupType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit, Func<IVectorGroupSpecializationType, bool> shouldInherit, bool onlyInherited = false)
    {
        HashSet<IUnitInstance> includedUnits = new(initialUnits);

        recurisvelyAdd(groupType, onlyInherited);

        return includedUnits;

        void recurisvelyAdd(IVectorGroupType group, bool onlyInherited = false)
        {
            if (onlyInherited is false)
            {
                modify(group);
            }

            recurse(group);
        }

        void modify(IVectorGroupType group)
        {
            if (group.UnitInstanceInclusions.Any())
            {
                includedUnits.IntersectWith(listUnits(group.UnitInstanceInclusions));

                return;
            }

            includedUnits.ExceptWith(listUnits(group.UnitInstanceExclusions));

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

        void recurse(IVectorGroupType group)
        {
            if (group is IVectorGroupSpecializationType vectorSpecialization && shouldInherit(vectorSpecialization))
            {
                recurisvelyAdd(vectorPopulation.Groups[vectorSpecialization.Definition.OriginalQuantity]);
            }
        }
    }

    private static SpecializedSharpMeasuresVectorGroupValidator SpecializedSharpMeasuresVectorGroupValidator { get; } = new(EmptySpecializedSharpMeasuresVectorGroupValidationDiagnostics.Instance);

    private static DerivedQuantityValidator DerivedQuantityValidator { get; } = new(EmptyDerivedQuantityValidationDiagnostics.Instance);
    private static ConvertibleVectorFilterer ConvertibleVectorFilterer { get; } = new(EmptyConvertibleVectorFilteringDiagnostics.Instance);

    private static IncludeUnitsFilterer IncludeUnitsFilterer { get; } = new(EmptyIncludeUnitsFilteringDiagnostics.Instance);
    private static ExcludeUnitsFilterer ExcludeUnitsFilterer { get; } = new(EmptyExcludeUnitsFilteringDiagnostics.Instance);
}
