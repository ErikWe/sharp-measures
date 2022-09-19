namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Linq;
using System.Threading;

internal static class ForeignTypeResolver
{
    public static IncrementalValueProvider<IResolvedScalarPopulation> Resolve(IncrementalValueProvider<IForeignScalarResolver> scalarResolver, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IResolvedScalarPopulation> unextendedScalarPopulation)
    {
        return scalarResolver.Combine(unitPopulation, scalarPopulation, unextendedScalarPopulation).Select(Resolve);
    }

    public static IncrementalValueProvider<IResolvedVectorPopulation> Resolve(IncrementalValueProvider<IForeignVectorResolver> vectorResolver, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation, IncrementalValueProvider<IResolvedVectorPopulation> unextendedVectorPopulation)
    {
        return vectorResolver.Combine(unitPopulation, vectorPopulation, unextendedVectorPopulation).Select(Resolve);
    }

    private static IResolvedScalarPopulation Resolve((IForeignScalarResolver ScalarResolver, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IResolvedScalarPopulation UnextendedScalarPopulation) input, CancellationToken _)
    {
        return input.ScalarResolver.ResolveAndExtend(input.UnitPopulation, input.ScalarPopulation, input.UnextendedScalarPopulation);
    }

    private static IResolvedVectorPopulation Resolve((IForeignVectorResolver VectorResolvers, IUnitPopulation UnitPopulation, IVectorPopulation VectorPopulation, IResolvedVectorPopulation UnextendedVectorPopulation) input, CancellationToken _)
    {
        return input.VectorResolvers.ResolveAndExtend(input.UnitPopulation, input.VectorPopulation, input.UnextendedVectorPopulation);
    }
}
