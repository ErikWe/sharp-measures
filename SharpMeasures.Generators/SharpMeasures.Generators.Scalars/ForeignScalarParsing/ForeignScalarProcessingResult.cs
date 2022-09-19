namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public interface IForeignScalarProcessingResult
{
    public abstract IReadOnlyList<IScalarBaseType> ScalarBases { get; }
    public abstract IReadOnlyList<IScalarSpecializationType> ScalarSpecializations { get; }
}

internal sealed record class ForeignScalarProcessingResult : IForeignScalarProcessingResult
{
    public IReadOnlyList<ScalarBaseType> ScalarBases { get; }
    public IReadOnlyList<ScalarSpecializationType> ScalarSpecializations { get; }

    IReadOnlyList<IScalarBaseType> IForeignScalarProcessingResult.ScalarBases => ScalarBases;
    IReadOnlyList<IScalarSpecializationType> IForeignScalarProcessingResult.ScalarSpecializations => ScalarSpecializations;

    public ForeignScalarProcessingResult(IReadOnlyList<ScalarBaseType> scalarBases, IReadOnlyList<ScalarSpecializationType> scalarSpecializations)
    {
        ScalarBases = scalarBases.AsReadOnlyEquatable();
        ScalarSpecializations = scalarSpecializations.AsReadOnlyEquatable();
    }
}
