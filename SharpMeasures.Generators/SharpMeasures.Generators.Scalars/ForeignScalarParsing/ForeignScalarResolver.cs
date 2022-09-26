namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units;

using System.Threading;

public sealed class ForeignScalarResolver
{
    public static IResolvedScalarPopulation ResolveAndExtend(ForeignScalarProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IResolvedScalarPopulation unextendedScalarPopulation, CancellationToken token)
    {
        var resolver = new ForeignScalarResolver();

        if (token.IsCancellationRequested is false)
        {
            resolver.Resolve(processingResult, unitPopulation, scalarPopulation);
        }

        return resolver.Extend(unextendedScalarPopulation);
    }

    private EquatableList<ResolvedScalarType> Scalars { get; } = new();

    private ForeignScalarResolver() { }

    private void Resolve(ForeignScalarProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation)
    {
        foreach (var processedScalarBase in processingResult.ScalarBases)
        {
            var scalar = ScalarBaseResolver.Resolve(processedScalarBase, unitPopulation);

            if (scalar.HasValue)
            {
                Scalars.Add(scalar.Value);
            }
        }

        foreach (var processedScalarSpecialization in processingResult.ScalarSpecializations)
        {
            var scalar = ScalarSpecializationResolver.Resolve(processedScalarSpecialization, unitPopulation, scalarPopulation);

            if (scalar.HasValue)
            {
                Scalars.Add(scalar.Value);
            }
        }
    }

    private ForeignScalarResolutionResult Finalize() => new(Scalars);
    private IResolvedScalarPopulation Extend(IResolvedScalarPopulation scalarPopulation) => ExtendedResolvedScalarPopulation.Build(scalarPopulation, Finalize());
}
