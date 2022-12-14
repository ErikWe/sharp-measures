namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal sealed class GroupMemberValidator
{
    private IVectorValidationDiagnosticsStrategy DiagnosticsStrategy { get; }

    public GroupMemberValidator(IVectorValidationDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<GroupMemberType> Validate((Optional<GroupMemberType> UnvalidatedVector, VectorProcessingData ProcessingData, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.UnvalidatedVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupMemberType>();
        }

        return Validate(input.UnvalidatedVector.Value, input.ProcessingData, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
    }

    public IOptionalWithDiagnostics<GroupMemberType> Validate(GroupMemberType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var vector = ValidateVector(vectorType, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<GroupMemberType>();
        }

        if (vectorPopulation.GroupBases.TryGetValue(vectorType.Definition.VectorGroup, out var groupBase) is false)
        {
            return vector.AsEmptyOptional<GroupMemberType>();
        }

        if (unitPopulation.Units.TryGetValue(groupBase.Definition.Unit, out var unit) is false)
        {
            return vector.AsEmptyOptional<GroupMemberType>();
        }

        var inheritedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => vector.Definition.InheritUnitsFromMembers,
            static (vector) => vector.Definition.InheritUnits, static (vector) => vector.Definition.InheritUnits, onlyInherited: true);

        var inheritedUnitInstanceNames = new HashSet<string>(inheritedUnitInstances.Select(static (unit) => unit.Name));

        var unitInstanceInclusions = CommonValidation.ValidateIncludeUnitInstances(vectorType.Type, unit, vectorType.UnitInstanceInclusions, inheritedUnitInstanceNames, DiagnosticsStrategy);
        var unitInstanceExclusions = CommonValidation.ValidateExcludeUnitInstances(vectorType.Type, unit, vectorType.UnitInstanceExclusions, inheritedUnitInstanceNames, DiagnosticsStrategy);

        var definedUnitInstances = GetUnitInstanceInclusions(vectorType, vectorPopulation, unit.UnitInstancesByName.Values, unit, static (vector) => false, static (vector) => false, static (vector) => false);

        var allUnits = inheritedUnitInstances.Concat(definedUnitInstances).ToList();

        var inheritedConstants = CollectInheritedItems(vectorType, vectorPopulation, static (vector) => vector.Constants, static (vector) => Array.Empty<IVectorConstant>(), static (vector) => vector.Definition.InheritConstantsFromMembers, static (vector) => false, static (vector) => false);

        var operations = CommonValidation.ValidateOperations(vectorType.Type, new[] { vectorType.Definition.Dimension }, vectorType.Operations, scalarPopulation, vectorPopulation, DiagnosticsStrategy);
        var vectorOperations = CommonValidation.ValidateVectorOperations(vectorType.Type, new[] { vectorType.Definition.Dimension }, vectorType.VectorOperations, scalarPopulation, vectorPopulation, DiagnosticsStrategy);
        var constants = CommonValidation.ValidateConstants(vectorType.Type, vectorType.Definition.Dimension, unit, allUnits, vectorType.Constants, inheritedConstants, DiagnosticsStrategy);
        var conversions = CommonValidation.ValidateConversions(vectorType.Type, vectorType.Definition.Dimension, vectorType.Conversions, vectorPopulation, DiagnosticsStrategy);

        GroupMemberType product = new(vectorType.Type, vector.Result, operations.Result, vectorOperations.Result, vectorType.Processes, constants.Result, conversions.Result, unitInstanceInclusions.Result, unitInstanceExclusions.Result);
        var allDiagnostics = vector.Concat(operations).Concat(vectorOperations).Concat(constants).Concat(conversions).Concat(unitInstanceInclusions).Concat(unitInstanceExclusions);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ValidateVector(GroupMemberType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorGroupMemberValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ValidityFilter.Create(SharpMeasuresVectorGroupMemberValidator).Validate(validationContext, vectorType.Definition).Transform(vectorType.Definition);
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

                var shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                var shouldInheritFromGroup = (originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember)) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyAddItems(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private static IEnumerable<IUnitInstance> GetUnitInstanceInclusions(GroupMemberType vectorType, IVectorPopulation vectorPopulation, IEnumerable<IUnitInstance> initialUnits, IUnitType unit,
        Func<IVectorGroupMemberType, bool> shouldMemberInheritFromMembers, Func<IVectorGroupMemberType, bool> shouldMemberInheritFromGroup, Func<IVectorGroupSpecializationType, bool> shouldGroupInherit, bool onlyInherited = false)
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
            if (inclusions.Count > 0)
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

                var shouldInheritFromMember = inheritedFromMember && (originalCorrespondingMember is null || shouldMemberInheritFromMembers(originalCorrespondingMember));
                var shouldInheritFromGroup = (originalCorrespondingMember is not null && shouldMemberInheritFromGroup(originalCorrespondingMember)) || shouldGroupInherit(vectorGroupSpecialization);

                recursivelyModify(originalVectorGroup, originalCorrespondingMember, shouldInheritFromMember, shouldInheritFromGroup);
            }
        }
    }

    private SharpMeasuresVectorGroupMemberValidator SharpMeasuresVectorGroupMemberValidator => new(DiagnosticsStrategy.SharpMeasuresVectorGroupMemberDiagnostics);
}
