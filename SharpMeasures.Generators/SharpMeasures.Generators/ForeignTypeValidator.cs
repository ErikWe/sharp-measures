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
    public static IncrementalValueProvider<IUnitPopulation> Validate(IncrementalValueProvider<IForeignUnitValidator> unitValidator, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IUnitPopulation> unextendedUnitPopulation)
    {
        return unitValidator.Combine(unitPopulation, scalarPopulation, unextendedUnitPopulation).Select(Validate);
    }

    public static (IncrementalValueProvider<IScalarPopulation> Population, IncrementalValueProvider<IForeignScalarResolver> Resolver) Validate(IncrementalValueProvider<IForeignScalarValidator> scalarValidator, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation, IncrementalValueProvider<IScalarPopulation> unextendedScalarPopulation)
    {
        var populationAndResolver = scalarValidator.Combine(unitPopulation, scalarPopulation, vectorPopulation, unextendedScalarPopulation).Select(Validate);

        return (populationAndResolver.Select(ExtractPopulation), populationAndResolver.Select(ExtractResolver));
    }

    public static (IncrementalValueProvider<IVectorPopulation> Population, IncrementalValueProvider<IForeignVectorResolver> Resolver) Validate(IncrementalValueProvider<IForeignVectorValidator> vectorValidator, IncrementalValueProvider<IUnitPopulation> unitPopulation, IncrementalValueProvider<IScalarPopulation> scalarPopulation, IncrementalValueProvider<IVectorPopulation> vectorPopulation, IncrementalValueProvider<IVectorPopulation> unextendedVectorPopulation)
    {
        var populationAndResolver = vectorValidator.Combine(unitPopulation, scalarPopulation, vectorPopulation, unextendedVectorPopulation).Select(Validate);

        return (populationAndResolver.Select(ExtractPopulation), populationAndResolver.Select(ExtractResolver));
    }

    private static IUnitPopulation Validate((IForeignUnitValidator UnitValidator, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IUnitPopulation UnextendedUnitPopulation) input, CancellationToken _)
    {
        return input.UnitValidator.ValidateAndExtend(input.UnitPopulation, input.ScalarPopulation, input.UnextendedUnitPopulation);
    }

    private static (IScalarPopulation Population, IForeignScalarResolver Resolver) Validate((IForeignScalarValidator ScalarValidator, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation, IScalarPopulation UnextendedScalarPopulation) input, CancellationToken _)
    {
        return input.ScalarValidator.ValidateAndExtend(input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, input.UnextendedScalarPopulation);
    }

    private static (IVectorPopulation Population, IForeignVectorResolver Resolver) Validate((IForeignVectorValidator VectorValidator, IUnitPopulation UnitPopulation, IScalarPopulation ScalarPopulation, IVectorPopulation VectorPopulation, IVectorPopulation UnextendedVectorPopulation) input, CancellationToken _)
    {
        return input.VectorValidator.ValidateAndExtend(input.UnitPopulation, input.ScalarPopulation, input.VectorPopulation, input.UnextendedVectorPopulation);
    }

    private static IScalarPopulation ExtractPopulation((IScalarPopulation Population, IForeignScalarResolver) input, CancellationToken _) => input.Population;
    private static IVectorPopulation ExtractPopulation((IVectorPopulation Population, IForeignVectorResolver) input, CancellationToken _) => input.Population;
    private static IForeignScalarResolver ExtractResolver((IScalarPopulation, IForeignScalarResolver Resolver) input, CancellationToken _) => input.Resolver;
    private static IForeignVectorResolver ExtractResolver((IVectorPopulation, IForeignVectorResolver Resolver) input, CancellationToken _) => input.Resolver;
}
