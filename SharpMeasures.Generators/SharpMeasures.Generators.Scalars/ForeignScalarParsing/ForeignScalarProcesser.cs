namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;

public interface IForeignScalarProcesser
{
    public abstract (IScalarPopulation Population, IForeignScalarValidator Validator) ProcessAndExtend(IScalarPopulation unextendedScalarPopulation);
}

internal sealed record class ForeignScalarProcesser : IForeignScalarProcesser
{
    private ForeignScalarParsingResult ParsingResult { get; }

    private EquatableList<ScalarBaseType> ScalarBases { get; } = new();
    private EquatableList<ScalarSpecializationType> ScalarSpecializations { get; } = new();

    public ForeignScalarProcesser(ForeignScalarParsingResult parsingResult)
    {
        ParsingResult = parsingResult;
    }

    public (IScalarPopulation, IForeignScalarValidator) ProcessAndExtend(IScalarPopulation unextendedScalarPopulation)
    {
        ForeignScalarBaseProcesser scalarBaseProcesser = new();
        ForeignScalarSpecializationProcesser scalarSpecializationProcesser = new();

        foreach (var rawScalarBase in ParsingResult.ScalarBases)
        {
            var scalarBase = scalarBaseProcesser.Process(rawScalarBase);

            if (scalarBase.HasValue)
            {
                ScalarBases.Add(scalarBase.Value);
            }
        }

        foreach (var rawScalarSpecialization in ParsingResult.ScalarSpecializations)
        {
            var scalarSpecialization = scalarSpecializationProcesser.Process(rawScalarSpecialization);

            if (scalarSpecialization.HasValue)
            {
                ScalarSpecializations.Add(scalarSpecialization.Value);
            }
        }

        var result = new ForeignScalarProcessingResult(ScalarBases, ScalarSpecializations);
        var extendedPopulation = ExtendedScalarPopulation.Build(unextendedScalarPopulation, result);
        var validator = new ForeignScalarValidator(result);

        return (extendedPopulation, validator);
    }
}
