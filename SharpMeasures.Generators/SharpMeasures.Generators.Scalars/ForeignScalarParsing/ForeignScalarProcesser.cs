namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Generators.Scalars.Parsing;

using System.Collections.Generic;
using System.Threading;

public sealed class ForeignScalarProcesser
{
    public static (ForeignScalarProcessingResult ProcessingResult, IScalarPopulation Population) ProcessAndExtend(ForeignScalarParsingResult parsingResult, IScalarPopulation unextendedScalarPopulation, CancellationToken token)
    {
        var processer = new ForeignScalarProcesser();

        if (token.IsCancellationRequested is false)
        {
            processer.Process(parsingResult);
        }

        return (processer.Finalize(), processer.Extend(unextendedScalarPopulation));
    }

    private ScalarBaseProcesser ScalarBaseProcesser { get; } = new(ScalarProcessingDiagnosticsStrategies.EmptyDiagnostics);
    private ScalarSpecializationProcesser ScalarSpecializationProcesser { get; } = new(ScalarProcessingDiagnosticsStrategies.EmptyDiagnostics);

    private List<ScalarBaseType> ScalarBases { get; } = new();
    private List<ScalarSpecializationType> ScalarSpecializations { get; } = new();

    private ForeignScalarProcesser() { }

    private void Process(ForeignScalarParsingResult parsingResult)
    {
        foreach (var rawScalarBase in parsingResult.ScalarBases)
        {
            var scalarBase = ScalarBaseProcesser.Process(rawScalarBase);

            if (scalarBase.HasResult)
            {
                ScalarBases.Add(scalarBase.Result);
            }
        }

        foreach (var rawScalarSpecialization in parsingResult.ScalarSpecializations)
        {
            var scalarSpecialization = ScalarSpecializationProcesser.Process(rawScalarSpecialization);

            if (scalarSpecialization.HasResult)
            {
                ScalarSpecializations.Add(scalarSpecialization.Result);
            }
        }
    }

    private ForeignScalarProcessingResult Finalize() => new(ScalarBases, ScalarSpecializations);
    private IScalarPopulation Extend(IScalarPopulation scalarPopulation) => ExtendedScalarPopulation.Build(scalarPopulation, Finalize());
}
