namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;
using System.Threading;

public sealed class ForeignScalarValidator
{
    public static (ForeignScalarProcessingResult ValidationResult, IScalarPopulation Population) ValidateAndExtend(ForeignScalarProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IScalarPopulation unextendedScalarPopulation, CancellationToken token)
    {
        var validator = new ForeignScalarValidator();

        if (token.IsCancellationRequested is false)
        {
            validator.Validate(processingResult, unitPopulation, scalarPopulation, vectorPopulation);
        }

        return (validator.Finalize(), validator.Extend(unextendedScalarPopulation));
    }

    private static ScalarBaseValidator ScalarBaseValidator { get; } = new(ScalarValidationDiagnosticsStrategies.EmptyDiagnostics);
    private static ScalarSpecializationValidator ScalarSpecializationValidator { get; } = new(ScalarValidationDiagnosticsStrategies.EmptyDiagnostics);

    private List<ScalarBaseType> ScalarBases { get; } = new();
    private List<ScalarSpecializationType> ScalarSpecializations { get; } = new();

    private ForeignScalarValidator() { }

    private void Validate(ForeignScalarProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        foreach (var processedScalarBase in processingResult.ScalarBases)
        {
            var scalarBase = ScalarBaseValidator.Validate(processedScalarBase, ScalarProcessingData.Empty,  unitPopulation, scalarPopulation, vectorPopulation);

            if (scalarBase.HasResult)
            {
                ScalarBases.Add(scalarBase.Result);
            }
        }

        foreach (var processedScalarSpecialization in processingResult.ScalarSpecializations)
        {
            var scalarSpecialization = ScalarSpecializationValidator.Validate(processedScalarSpecialization, ScalarProcessingData.Empty, unitPopulation, scalarPopulation, vectorPopulation);

            if (scalarSpecialization.HasResult)
            {
                ScalarSpecializations.Add(scalarSpecialization.Result);
            }
        }
    }

    private ForeignScalarProcessingResult Finalize() => new(ScalarBases, ScalarSpecializations);
    private IScalarPopulation Extend(IScalarPopulation scalarPopulation) => ExtendedScalarPopulation.Build(scalarPopulation, Finalize());
}
