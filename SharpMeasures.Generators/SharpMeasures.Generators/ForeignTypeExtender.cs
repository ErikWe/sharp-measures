namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Populations;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Threading;

internal static class ForeignTypeExtender
{
    public static (IncrementalValueProvider<IUnitPopulation>, IncrementalValueProvider<IScalarPopulation>, IncrementalValueProvider<IVectorPopulation>) Extend(IncrementalValueProvider<ForeignTypes> foreignTypesProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var populationsProvider = foreignTypesProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(extend);

        return (populationsProvider.Select(ExtractUnitPopulation), populationsProvider.Select(ExtractScalarPopulation), populationsProvider.Select(ExtractVectorPopulation));

        static (IUnitPopulation, IScalarPopulation, IVectorPopulation) extend((ForeignTypes ForeignTypes, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _)
        {
            return Extend(input.ForeignTypes, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        }

        static IUnitPopulation ExtractUnitPopulation((IUnitPopulation UnitPopulation, IScalarPopulation, IVectorPopulation) input, CancellationToken _) => input.UnitPopulation;
        static IScalarPopulation ExtractScalarPopulation((IUnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation) input, CancellationToken _) => input.ScalarPopulation;
        static IVectorPopulation ExtractVectorPopulation((IUnitPopulation, IScalarPopulation, IVectorPopulation VectorPopulation) input, CancellationToken _) => input.VectorPopulation;
    }

    public static (IncrementalValueProvider<IUnitPopulation>, IncrementalValueProvider<IResolvedScalarPopulation>, IncrementalValueProvider<IResolvedVectorPopulation>) Extend(IncrementalValueProvider<ResolvedForeignTypes> foreignTypesProvider, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IResolvedScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IResolvedVectorPopulation> vectorPopulationProvider)
    {
        var populationsProvider = foreignTypesProvider.Combine(unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(extend);

        return (populationsProvider.Select(ExtractUnitPopulation), populationsProvider.Select(ExtractScalarPopulation), populationsProvider.Select(ExtractVectorPopulation));

        static (IUnitPopulation, IResolvedScalarPopulation, IResolvedVectorPopulation) extend((ResolvedForeignTypes ForeignTypes, IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _)
        {
            return Extend(input.ForeignTypes, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation);
        }

        static IUnitPopulation ExtractUnitPopulation((IUnitPopulation UnitPopulation, IResolvedScalarPopulation, IResolvedVectorPopulation) input, CancellationToken _) => input.UnitPopulation;
        static IResolvedScalarPopulation ExtractScalarPopulation((IUnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation) input, CancellationToken _) => input.ScalarPopulation;
        static IResolvedVectorPopulation ExtractVectorPopulation((IUnitPopulation, IResolvedScalarPopulation, IResolvedVectorPopulation VectorPopulation) input, CancellationToken _) => input.VectorPopulation;
    }

    public static (IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation) Extend(ForeignTypes foreignTypes, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var extendedUnitPopulation = ExtendedUnitPopulation.Build(unitPopulation, foreignTypes.Units);
        var extendedScalarPopulation = ExtendedScalarPopulation.Build(scalarPopulation, foreignTypes.ScalarBases, foreignTypes.ScalarSpecializations);
        var extendedVectorPopulation = ExtendedVectorPopulation.Build(vectorPopulation, foreignTypes.VectorBases, foreignTypes.VectorSpecializations, foreignTypes.GroupBases, foreignTypes.GroupSpecializations, foreignTypes.GroupMembers);

        return (extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);
    }

    public static (IUnitPopulation UnitPopulation, IResolvedScalarPopulation ScalarPopulation, IResolvedVectorPopulation VectorPopulation) Extend(ResolvedForeignTypes foreignTypes, IUnitPopulation unitPopulation, IResolvedScalarPopulation scalarPopulation, IResolvedVectorPopulation vectorPopulation)
    {
        var extendedUnitPopulation = ExtendedUnitPopulation.Build(unitPopulation, foreignTypes.Units);
        var extendedScalarPopulation = ExtendedResolvedScalarPopulation.Build(scalarPopulation, foreignTypes.Scalars);
        var extendedVectorPopulation = ExtendedResolvedVectorPopulation.Build(vectorPopulation, foreignTypes.Groups, foreignTypes.Vectors);

        return (extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);
    }
}
