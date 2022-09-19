namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal sealed record class ExtendedResolvedScalarPopulation : IResolvedScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedScalarType> Scalars { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IResolvedQuantityType);

    private ExtendedResolvedScalarPopulation(IReadOnlyDictionary<NamedType, IResolvedScalarType> scalars)
    {
        Scalars = scalars.AsReadOnlyEquatable();
    }

    public static ExtendedResolvedScalarPopulation Build(IResolvedScalarPopulation originalPopulation, ForeignScalarResolutionResult resolutionResult)
    {
        Dictionary<NamedType, IResolvedScalarType> population = new();

        foreach (var keyValue in originalPopulation.Scalars)
        {
            population.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var scalar in resolutionResult.Scalars)
        {
            population.TryAdd(scalar.Type.AsNamedType(), scalar);
        }

        return new(population);
    }
}
