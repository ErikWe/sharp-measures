namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Threading;

internal static class ForeignTypeResolver
{
    public static IncrementalValueProvider<IResolvedScalarPopulation> Resolve(IncrementalValueProvider<ForeignScalarProcessingResult> processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IResolvedScalarPopulation> unextendedScalarPopulation)
    {
        return processingResult.Combine(unitPopulation, scalarPopulation, unextendedScalarPopulation).Select(Resolve);
    }

    public static IncrementalValueProvider<IResolvedVectorPopulation> Resolve(IncrementalValueProvider<ForeignVectorProcessingResult> processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation, IncrementalValueProvider<IResolvedVectorPopulation> unextendedVectorPopulation)
    {
        return processingResult.Combine(unitPopulation, vectorPopulation, unextendedVectorPopulation).Select(Resolve);
    }

    private static IResolvedScalarPopulation Resolve((ForeignScalarProcessingResult ProcessingResult, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IResolvedScalarPopulation UnextendedScalarPopulation) input, CancellationToken token)
    {
        return ForeignScalarResolver.ResolveAndExtend(input.ProcessingResult, input.UnitPopulation, input.ScalarPopulation, input.UnextendedScalarPopulation, token);
    }

    private static IResolvedVectorPopulation Resolve((ForeignVectorProcessingResult ProcessingResult, IUnitPopulation UnitPopulation, IVectorPopulation VectorPopulation, IResolvedVectorPopulation UnextendedVectorPopulation) input, CancellationToken token)
    {
        return ForeignVectorResolver.ResolveAndExtend(input.ProcessingResult, input.UnitPopulation, input.VectorPopulation, input.UnextendedVectorPopulation, token);
    }
}
