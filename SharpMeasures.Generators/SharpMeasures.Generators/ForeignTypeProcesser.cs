namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.ForeignUnitParsing;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Threading;

internal static class ForeignTypeProcesser
{
    public static (IncrementalValueProvider<ForeignUnitProcessingResult> ProcessingResult, IncrementalValueProvider<IUnitPopulation> Population) Process(IncrementalValueProvider<ForeignUnitParsingResult> parsingResult, IncrementalValueProvider<IUnitPopulation> unitPopulation)
    {
        var populationAndValidator = parsingResult.Combine(unitPopulation).Select(Process);

        return (populationAndValidator.Select(ExtractProcessingResult), populationAndValidator.Select(ExtractPopulation));
    }

    public static (IncrementalValueProvider<ForeignScalarProcessingResult> ProcessingResult, IncrementalValueProvider<IScalarPopulation> Population) Process(IncrementalValueProvider<ForeignScalarParsingResult> parsingResult, IncrementalValueProvider<IScalarPopulation> scalarPopulation)
    {
        var populationAndValidator = parsingResult.Combine(scalarPopulation).Select(Process);

        return (populationAndValidator.Select(ExtractProcessingResult), populationAndValidator.Select(ExtractPopulation));
    }

    public static (IncrementalValueProvider<ForeignVectorProcessingResult> ProcessingResult, IncrementalValueProvider<IVectorPopulation> Population) Process(IncrementalValueProvider<ForeignVectorParsingResult> parsingResult, IncrementalValueProvider<IVectorPopulation> vectorPopulation)
    {
        var populationAndValidator = parsingResult.Combine(vectorPopulation).Select(Process);

        return (populationAndValidator.Select(ExtractProcessingResult), populationAndValidator.Select(ExtractPopulation));
    }

    private static (ForeignUnitProcessingResult ProcessingResult, IUnitPopulation Population) Process((ForeignUnitParsingResult ParsingResult, IUnitPopulation UnitPopulation) input, CancellationToken token)
    {
        return ForeignUnitProcesser.ProcessAndExtend(input.ParsingResult, input.UnitPopulation, token);
    }

    private static (ForeignScalarProcessingResult ProcessingResult, IScalarPopulation Population) Process((ForeignScalarParsingResult ParsingResult, IScalarPopulation ScalarPopulation) input, CancellationToken token)
    {
        return ForeignScalarProcesser.ProcessAndExtend(input.ParsingResult, input.ScalarPopulation, token);
    }

    private static (ForeignVectorProcessingResult ProcessingResult, IVectorPopulation Population) Process((ForeignVectorParsingResult ParsingResult, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        return ForeignVectorProcesser.ProcessAndExtend(input.ParsingResult, input.VectorPopulation, token);
    }

    private static TPopulation ExtractPopulation<TPopulation, T>((T, TPopulation Population) input, CancellationToken _) => input.Population;
    private static TProcessingResult ExtractProcessingResult<TProcessingResult, T>((TProcessingResult ProcessingResult, T) input, CancellationToken _) => input.ProcessingResult;
}
