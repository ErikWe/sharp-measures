namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal class ForeignTypeResolver
{
    public static IncrementalValueProvider<ResolvedForeignTypes> Resolve(IncrementalValueProvider<ForeignTypes> foreignTypes, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation)
    {
        return foreignTypes.Combine(unitPopulation, scalarPopulation, vectorPopulation).Select(Resolve);
    }

    private static ResolvedForeignTypes Resolve((ForeignTypes ForeignTypes, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
    {
        ForeignTypeResolver resolver = new(input.ForeignTypes, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);

        return resolver.Resolve();
    }

    private ForeignTypes ForeignTypes { get; }

    private IUnitPopulation UnitPopulation { get; }
    private IScalarPopulation ScalarPopulation { get; }
    private IVectorPopulation VectorPopulation { get; }

    private List<IResolvedScalarType> ResolvedScalars { get; } = new();
    private List<IResolvedVectorGroupType> ResolvedGroups { get; } = new();
    private List<IResolvedVectorType> ResolvedVectors { get; } = new();

    public ForeignTypeResolver(ForeignTypes foreignTypes, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        ForeignTypes = foreignTypes;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }

    private ResolvedForeignTypes Resolve()
    {
        foreach (var scalar in ForeignTypes.ScalarBases)
        {
            var resolved = ForeignScalarBaseResolver.Resolve(scalar, UnitPopulation);

            if (resolved.HasValue)
            {
                ResolvedScalars.Add(resolved.Value);
            }
        }

        foreach (var scalar in ForeignTypes.ScalarSpecializations)
        {
            var resolved = ForeignScalarSpecializationResolver.Resolve(scalar, UnitPopulation, ScalarPopulation);

            if (resolved.HasValue)
            {
                ResolvedScalars.Add(resolved.Value);
            }
        }

        foreach (var group in ForeignTypes.GroupBases)
        {
            var resolved = ForeignGroupBaseResolver.Resolve(group, UnitPopulation, VectorPopulation);

            if (resolved.HasValue)
            {
                ResolvedGroups.Add(resolved.Value);
            }
        }

        foreach (var group in ForeignTypes.GroupSpecializations)
        {
            var resolved = ForeignGroupSpecializationResolver.Resolve(group, UnitPopulation, VectorPopulation);

            if (resolved.HasValue)
            {
                ResolvedGroups.Add(resolved.Value);
            }
        }

        foreach (var member in ForeignTypes.GroupMembers)
        {
            var resolved = ForeignGroupMemberResolver.Resolve(member, UnitPopulation, VectorPopulation);

            if (resolved.HasValue)
            {
                ResolvedVectors.Add(resolved.Value);
            }
        }

        foreach (var vector in ForeignTypes.VectorBases)
        {
            var resolved = ForeignVectorBaseResolver.Resolve(vector, UnitPopulation);

            if (resolved.HasValue)
            {
                ResolvedVectors.Add(resolved.Value);
            }
        }

        foreach (var vector in ForeignTypes.VectorSpecializations)
        {
            var resolved = ForeignVectorSpecializationResolver.Resolve(vector, UnitPopulation, VectorPopulation);

            if (resolved.HasValue)
            {
                ResolvedVectors.Add(resolved.Value);
            }
        }

        return new(ForeignTypes.Units, ResolvedScalars, ResolvedGroups, ResolvedVectors);
    }
}
