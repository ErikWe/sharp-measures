namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

public interface IForeignScalarValidator
{
    public abstract (IScalarPopulation Population, IForeignScalarResolver Resolver) ValidateAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IScalarPopulation unextendedScalarPopulation);
}

internal sealed record class ForeignScalarValidator : IForeignScalarValidator
{
    private ForeignScalarProcessingResult ProcessingResult { get; }

    private EquatableList<ScalarBaseType> ScalarBases { get; } = new();
    private EquatableList<ScalarSpecializationType> ScalarSpecializations { get; } = new();

    public ForeignScalarValidator(ForeignScalarProcessingResult parsingResult)
    {
        ProcessingResult = parsingResult;
    }

    public (IScalarPopulation, IForeignScalarResolver) ValidateAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IScalarPopulation unextendedScalarPopulation)
    {
        foreach (var processedScalarBase in ProcessingResult.ScalarBases)
        {
            var scalarBase = ForeignScalarBaseValidator.Validate(processedScalarBase, unitPopulation, scalarPopulation, vectorPopulation);

            if (scalarBase.HasValue)
            {
                ScalarBases.Add(scalarBase.Value);
            }
        }

        foreach (var processedScalarSpecialization in ProcessingResult.ScalarSpecializations)
        {
            var scalarSpecialization = ForeignScalarSpecializationValidator.Validate(processedScalarSpecialization, unitPopulation, scalarPopulation, vectorPopulation);

            if (scalarSpecialization.HasValue)
            {
                ScalarSpecializations.Add(scalarSpecialization.Value);
            }
        }

        var result = new ForeignScalarProcessingResult(ScalarBases, ScalarSpecializations);
        var extendedPopulation = ExtendedScalarPopulation.Build(unextendedScalarPopulation, result);
        var resolver = new ForeignScalarResolver(result);

        return (extendedPopulation, resolver);
    }
}
