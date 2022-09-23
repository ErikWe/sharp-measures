namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Threading;

public interface IForeignScalarResolver
{
    public abstract IResolvedScalarPopulation ResolveAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IResolvedScalarPopulation unextendedScalarPopulation, CancellationToken token);
}

internal sealed record class ForeignScalarResolver : IForeignScalarResolver
{
    private ForeignScalarProcessingResult ProcessingResult { get; }

    private EquatableList<ResolvedScalarType> Scalars { get; } = new();

    public ForeignScalarResolver(ForeignScalarProcessingResult parsingResult)
    {
        ProcessingResult = parsingResult;
    }

    public IResolvedScalarPopulation ResolveAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IResolvedScalarPopulation unextendedScalarPopulation, CancellationToken token)
    {
        if (token.IsCancellationRequested is false)
        {
            foreach (var processedScalarBase in ProcessingResult.ScalarBases)
            {
                var scalar = ForeignScalarBaseResolver.Resolve(processedScalarBase, unitPopulation);

                if (scalar.HasValue)
                {
                    Scalars.Add(scalar.Value);
                }
            }

            foreach (var processedScalarSpecialization in ProcessingResult.ScalarSpecializations)
            {
                var scalar = ForeignScalarSpecializationResolver.Resolve(processedScalarSpecialization, unitPopulation, scalarPopulation);

                if (scalar.HasValue)
                {
                    Scalars.Add(scalar.Value);
                }
            }
        }

        var result = new ForeignScalarResolutionResult(Scalars);
        return ExtendedResolvedScalarPopulation.Build(unextendedScalarPopulation, result);
    }
}
