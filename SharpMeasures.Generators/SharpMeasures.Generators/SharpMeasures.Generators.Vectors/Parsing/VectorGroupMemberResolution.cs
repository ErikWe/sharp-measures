namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class VectorGroupMemberResolution
{
    public static IOptionalWithDiagnostics<VectorGroupMemberType> Reduce
        ((IntermediateVectorGroupMemberType Intermediate, IIntermediateVectorGroupPopulation Population) vectors, CancellationToken _)
        => Reduce(vectors.Intermediate, vectors.Population);

    public static IOptionalWithDiagnostics<VectorGroupMemberType> Reduce(IntermediateVectorGroupMemberType intermediateMember,
        IIntermediateVectorGroupPopulation vectorPopulation)
    {
        var membersByDimension = ResolveMembersByDimension(intermediateMember, vectorPopulation);

        var derivations = ResolveCollection(intermediateMember, vectorPopulation, static (vector) => vector.Definition.InheritDerivations,
            static (vector) => vector.Derivations, static (vector) => vector.Derivations);

        var constants = ResolveCollection(intermediateMember, vectorPopulation, static (vector) => vector.Definition.InheritConstants,
            (vector) => vector.RegisteredMembersByDimension.Values.SelectMany(vectorGroupConstants),
            (vector) => vector.RegisteredMembersByDimension.Values.SelectMany(vectorGroupConstants));

        IEnumerable<IVectorConstant> vectorGroupConstants(IRegisteredVectorGroupMember member)
        {
            if (vectorPopulation.VectorGroupMembers.TryGetValue(member.Vector.Type.AsNamedType(), out var memberType))
            {
                return memberType.Constants;
            }

            return Array.Empty<IVectorConstant>();
        }

        var conversions = ResolveCollection(intermediateMember, vectorPopulation, static (vector) => vector.Definition.InheritConversions,
            static (vector) => vector.Conversions, static (vector) => vector.Conversions);

        var includedUnits = GetIncludedUnits(intermediateMember, intermediateMember.Definition.Unit, vectorPopulation, static (vector) => vector.Definition.InheritUnits,
            static (vector) => vector.UnitInclusions, static (vector) => vector.UnitExclusions, static (vector) => vector.IncludedUnits,
            static (vector) => Array.Empty<IUnresolvedUnitInstance>());

        VectorGroupMemberType reduced = new(intermediateMember.Type, intermediateMember.TypeLocation, intermediateMember.Definition, membersByDimension,
            derivations, constants, conversions, includedUnits);

        return OptionalWithDiagnostics.Result(reduced);
    }

    public static IOptionalWithDiagnostics<IntermediateVectorGroupMemberType> Resolve((UnresolvedVectorGroupMemberType Member, IUnresolvedUnitPopulation UnitPopulation,
        IUnresolvedScalarPopulation ScalarPopulation, IUnresolvedVectorPopulationWithData VectorPopulation) input, CancellationToken _)
        => Resolve(input.Member, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

    public static IOptionalWithDiagnostics<IntermediateVectorGroupMemberType> Resolve(UnresolvedVectorGroupMemberType unresolvedMember, IUnresolvedUnitPopulation unitPopulation,
        IUnresolvedScalarPopulation scalarPopulation, IUnresolvedVectorPopulationWithData vectorPopulation)
    {
        SharpMeasuresVectorGroupMemberResolutionContext memberResolutionContext = new(unresolvedMember.Type, unitPopulation, scalarPopulation, vectorPopulation);

        var member = SharpMeasuresVectorGroupMemberResolver.Process(memberResolutionContext, unresolvedMember.Definition);
        var allDiagnostics = member.Diagnostics;

        if (member.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<IntermediateVectorGroupMemberType>(allDiagnostics);
        }

        if (vectorPopulation.VectorGroupBases.TryGetValue(member.Result.VectorGroup.Type.AsNamedType(), out var baseVectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<IntermediateVectorGroupMemberType>(allDiagnostics);
        }

        if (unitPopulation.Units.TryGetValue(baseVectorGroup.Definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IntermediateVectorGroupMemberType>(allDiagnostics);
        }

        var constants = ResolveConstants(unresolvedMember.Type, unresolvedMember.Constants, unit, unresolvedMember.Definition.Dimension);

        allDiagnostics = allDiagnostics.Concat(constants.Diagnostics);

        IntermediateVectorGroupMemberType product = new(unresolvedMember.Type, unresolvedMember.TypeLocation, member.Result, constants.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ResolveConstants(DefinedType type,
        IEnumerable<UnresolvedVectorConstantDefinition> unresolvedConstants, IUnresolvedUnitType unit, int dimension)
    {
        VectorConstantResolutionContext vectorConstantResolutionContext = new(type, unit, dimension);

        return ProcessingFilter.Create(VectorConstantResolver).Filter(vectorConstantResolutionContext, unresolvedConstants);
    }

    private static IReadOnlyDictionary<int, IRegisteredVectorGroupMember> ResolveMembersByDimension(IntermediateVectorGroupMemberType member,
        IIntermediateVectorGroupPopulation vectorPopulation)
    {
        if (vectorPopulation.VectorGroupBases.TryGetValue(member.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroupBase))
        {
            return vectorGroupBase.RegisteredMembersByDimension;
        }

        if (vectorPopulation.VectorGroupSpecializations.TryGetValue(member.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroupSpecialization))
        {
            return vectorGroupSpecialization.RegisteredMembersByDimension;
        }

        return new Dictionary<int, IRegisteredVectorGroupMember>();
    }

    private static IReadOnlyList<IUnresolvedUnitInstance> GetIncludedUnits(IntermediateVectorGroupMemberType member, IUnresolvedUnitType unit,
        IIntermediateVectorGroupPopulation vectorPopulation, Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationInclusions,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationExclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseInclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseExclusions)
    {
        if (vectorPopulation.VectorGroupBases.TryGetValue(member.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroupBase))
        {
            return GetIncludedUnits(vectorGroupBase, unit, baseInclusions, baseExclusions);
        }

        if (vectorPopulation.VectorGroupSpecializations.TryGetValue(member.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroupSpecialization))
        {
            return GetIncludedUnits(vectorGroupSpecialization, unit, vectorPopulation, shouldInherit, specializationInclusions, specializationExclusions, baseInclusions, baseExclusions);
        }

        return Array.Empty<IUnresolvedUnitInstance>();
    }

    private static IReadOnlyList<IUnresolvedUnitInstance> GetIncludedUnits(IVectorGroupType vector, IUnresolvedUnitType unit,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseInclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseExclusions)
    {
        HashSet<IUnresolvedUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        performModification(baseInclusions(vector), baseExclusions(vector));

        return includedUnits.ToList();

        void performModification(IEnumerable<IUnresolvedUnitInstance> inclusions, IEnumerable<IUnresolvedUnitInstance> exclusions)
        {
            if (inclusions.Any())
            {
                includedUnits.IntersectWith(inclusions);
            }
            else
            {
                includedUnits.ExceptWith(exclusions);
            }
        }
    }

    private static IReadOnlyList<IUnresolvedUnitInstance> GetIncludedUnits(IIntermediateVectorGroupSpecializationType vector, IUnresolvedUnitType unit,
        IIntermediateVectorGroupPopulation vectorPopulation, Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationInclusions,
        Func<IIntermediateVectorGroupSpecializationType, IEnumerable<IUnresolvedUnitInstance>> specializationExclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseInclusions,
        Func<IVectorGroupType, IEnumerable<IUnresolvedUnitInstance>> baseExclusions)
    {
        HashSet<IUnresolvedUnitInstance> includedUnits = new(unit.UnitsByName.Values);

        recursivelyModify(vector);

        return includedUnits.ToList();

        void recursivelyModify(IIntermediateVectorGroupSpecializationType vector)
        {
            if (shouldInherit(vector))
            {
                if (vectorPopulation.VectorGroupSpecializations.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var originalVector))
                {
                    recursivelyModify(originalVector);
                }
                else if (vectorPopulation.VectorGroupBases.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var baseVector))
                {
                    performModification(baseInclusions(baseVector), baseExclusions(baseVector));
                }
            }

            performModification(specializationInclusions(vector), specializationExclusions(vector));
        }

        void performModification(IEnumerable<IUnresolvedUnitInstance> inclusions, IEnumerable<IUnresolvedUnitInstance> exclusions)
        {
            if (inclusions.Any())
            {
                includedUnits.IntersectWith(inclusions);
            }
            else
            {
                includedUnits.ExceptWith(exclusions);
            }
        }
    }

    private static IReadOnlyList<T> ResolveCollection<T>(IntermediateVectorGroupMemberType member, IIntermediateVectorGroupPopulation vectorPopulation,
        Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit, Func<IIntermediateVectorGroupSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IVectorGroupType, IEnumerable<T>> baseTransform)
    {
        if (vectorPopulation.VectorGroupBases.TryGetValue(member.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroupBase))
        {
            return ResolveCollection(vectorGroupBase, baseTransform);
        }

        if (vectorPopulation.VectorGroupSpecializations.TryGetValue(member.Definition.VectorGroup.Type.AsNamedType(), out var vectorGroupSpecialization))
        {
            return ResolveCollection(vectorGroupSpecialization, vectorPopulation, shouldInherit, specializationTransform, baseTransform);
        }

        return Array.Empty<T>();
    }

    private static IReadOnlyList<T> ResolveCollection<T>(IVectorGroupType vector, Func<IVectorGroupType, IEnumerable<T>> baseTransform)
    {
        List<T> items = new();

        items.AddRange(baseTransform(vector));

        return items;
    }

    private static IReadOnlyList<T> ResolveCollection<T>(IIntermediateVectorGroupSpecializationType vector, IIntermediateVectorGroupPopulation vectorPopulation,
        Func<IIntermediateVectorGroupSpecializationType, bool> shouldInherit, Func<IIntermediateVectorGroupSpecializationType, IEnumerable<T>> specializationTransform,
        Func<IVectorGroupType, IEnumerable<T>> baseTransform)
    {
        List<T> items = new();

        recursivelyAdd(vector);

        return items;

        void recursivelyAdd(IIntermediateVectorGroupSpecializationType vector)
        {
            items.AddRange(specializationTransform(vector));

            if (shouldInherit(vector))
            {
                if (vectorPopulation.VectorGroupSpecializations.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var originalVector))
                {
                    recursivelyAdd(originalVector);
                    return;
                }

                if (vectorPopulation.VectorGroupBases.TryGetValue(vector.Definition.OriginalVectorGroup.Type.AsNamedType(), out var baseVector))
                {
                    items.AddRange(baseTransform(baseVector));
                }
            }
        }
    }

    private static SharpMeasuresVectorGroupMemberResolver SharpMeasuresVectorGroupMemberResolver { get; } = new(SharpMeasuresVectorGroupMemberResolutionDiagnostics.Instance);

    private static VectorConstantResolver VectorConstantResolver { get; } = new(VectorConstantResolutionDiagnostics.Instance);
}
