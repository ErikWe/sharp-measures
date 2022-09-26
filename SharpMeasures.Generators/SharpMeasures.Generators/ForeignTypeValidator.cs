namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.ForeignUnitParsing;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using System.Linq;
using System.Threading;

internal static class ForeignTypeValidator
{
    public static IncrementalValueProvider<IUnitPopulation> Validate(IncrementalValueProvider<ForeignUnitProcessingResult> processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IUnitPopulation> unextendedUnitPopulation)
    {
        return processingResult.Combine(unitPopulation, scalarPopulation, unextendedUnitPopulation).Select(Validate);
    }

    public static (IncrementalValueProvider<ForeignScalarProcessingResult> ValidationResult, IncrementalValueProvider<IScalarPopulation> Population) Validate(IncrementalValueProvider<ForeignScalarProcessingResult> processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation, IncrementalValueProvider<IScalarPopulation> unextendedScalarPopulation)
    {
        var populationAndResolver = processingResult.Combine(unitPopulation, scalarPopulation, vectorPopulation, unextendedScalarPopulation).Select(Validate);

        return (populationAndResolver.Select(ExtractValidationResult), populationAndResolver.Select(ExtractPopulation));
    }

    public static (IncrementalValueProvider<ForeignVectorProcessingResult> ValidationResult, IncrementalValueProvider<IVectorPopulation> Population) Validate(IncrementalValueProvider<ForeignVectorProcessingResult> processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation, IncrementalValueProvider<IVectorPopulation> unextendedVectorPopulation)
    {
        var populationAndResolver = processingResult.Combine(unitPopulation, scalarPopulation, vectorPopulation, unextendedVectorPopulation).Select(Validate);

        return (populationAndResolver.Select(ExtractValidationResult), populationAndResolver.Select(ExtractPopulation));
    }

    private static IUnitPopulation Validate((ForeignUnitProcessingResult ProcessingResult, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IUnitPopulation UnextendedUnitPopulation) input, CancellationToken token)
    {
        return ForeignUnitValidator.ValidateAndExtend(input.ProcessingResult, input.UnitPopulation, input.ScalarPopulation, input.UnitPopulation, token);
    }

    private static (ForeignScalarProcessingResult ValidationResult, IScalarPopulation Population) Validate((ForeignScalarProcessingResult ProcessingResult, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation, IScalarPopulation UnextendedScalarPopulation) input, CancellationToken token)
    {
        return ForeignScalarValidator.ValidateAndExtend(input.ProcessingResult, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, input.UnextendedScalarPopulation, token);
    }

    private static (ForeignVectorProcessingResult ValidationResult, IVectorPopulation Population) Validate((ForeignVectorProcessingResult ProcessingResult, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation, IVectorPopulation UnextendedVectorPopulation) input, CancellationToken token)
    {
        return ForeignVectorValidator.ValidateAndExtend(input.ProcessingResult, input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, input.UnextendedVectorPopulation, token);
    }

    private static TPopulation ExtractPopulation<TPopulation, T>((T, TPopulation Population) input, CancellationToken _) => input.Population;
    private static TResult ExtractValidationResult<TResult, T>((TResult ValidationResult, T) input, CancellationToken _) => input.ValidationResult;
}
