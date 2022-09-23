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

internal static class ForeignTypeProcesser
{
    public static (IncrementalValueProvider<IUnitPopulation> Population, IncrementalValueProvider<IForeignUnitValidator> Validator) Process(IncrementalValueProvider<IForeignUnitProcesser> unitProcesser, IncrementalValueProvider<IUnitPopulation> unitPopulation)
    {
        var populationAndValidator = unitProcesser.Combine(unitPopulation).Select(Process);

        return (populationAndValidator.Select(ExtractPopulation), populationAndValidator.Select(ExtractValidator));
    }

    public static (IncrementalValueProvider<IScalarPopulation> Population, IncrementalValueProvider<IForeignScalarValidator> Validator) Process(IncrementalValueProvider<IForeignScalarProcesser> scalarProcesser, IncrementalValueProvider<IScalarPopulation> scalarPopulation)
    {
        var populationAndValidator = scalarProcesser.Combine(scalarPopulation).Select(Process);

        return (populationAndValidator.Select(ExtractPopulation), populationAndValidator.Select(ExtractValidator));
    }

    public static (IncrementalValueProvider<IVectorPopulation> Population, IncrementalValueProvider<IForeignVectorValidator> Validator) Process(IncrementalValueProvider<IForeignVectorProcesser> vectorProcesser, IncrementalValueProvider<IVectorPopulation> vectorPopulation)
    {
        var populationAndValidator = vectorProcesser.Combine(vectorPopulation).Select(Process);

        return (populationAndValidator.Select(ExtractPopulation), populationAndValidator.Select(ExtractValidator));
    }

    private static (IUnitPopulation Population, IForeignUnitValidator Validator) Process((IForeignUnitProcesser UnitProcesser, IUnitPopulation UnitPopulation) input, CancellationToken token)
    {
        return input.UnitProcesser.ProcessAndExtend(input.UnitPopulation, token);
    }

    private static (IScalarPopulation Population, IForeignScalarValidator Validator) Process((IForeignScalarProcesser ScalarProcesser, IScalarPopulation ScalarPopulation) input, CancellationToken token)
    {
        return input.ScalarProcesser.ProcessAndExtend(input.ScalarPopulation, token);
    }

    private static (IVectorPopulation Population, IForeignVectorValidator Validator) Process((IForeignVectorProcesser VectorProcesser, IVectorPopulation VectorPopulation) input, CancellationToken token)
    {
        return input.VectorProcesser.ProcessAndExtend(input.VectorPopulation, token);
    }

    private static IUnitPopulation ExtractPopulation((IUnitPopulation Population, IForeignUnitValidator) input, CancellationToken _) => input.Population;
    private static IScalarPopulation ExtractPopulation((IScalarPopulation Population, IForeignScalarValidator) input, CancellationToken _) => input.Population;
    private static IVectorPopulation ExtractPopulation((IVectorPopulation Population, IForeignVectorValidator) input, CancellationToken _) => input.Population;
    private static IForeignUnitValidator ExtractValidator((IUnitPopulation, IForeignUnitValidator Validator) input, CancellationToken _) => input.Validator;
    private static IForeignScalarValidator ExtractValidator((IScalarPopulation, IForeignScalarValidator Validator) input, CancellationToken _) => input.Validator;
    private static IForeignVectorValidator ExtractValidator((IVectorPopulation, IForeignVectorValidator Validator) input, CancellationToken _) => input.Validator;
}
