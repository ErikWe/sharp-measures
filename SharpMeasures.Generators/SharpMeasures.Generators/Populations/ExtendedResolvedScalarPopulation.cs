namespace SharpMeasures.Generators.Populations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal class ExtendedResolvedScalarPopulation : IResolvedScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedScalarType> Scalars => scalars;

    private ReadOnlyEquatableDictionary<NamedType, IResolvedScalarType> scalars { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IResolvedQuantityType);

    private ExtendedResolvedScalarPopulation(IReadOnlyDictionary<NamedType, IResolvedScalarType> scalars)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
    }

    public static ExtendedResolvedScalarPopulation Build(IResolvedScalarPopulation originalPopulation, IReadOnlyList<IResolvedScalarType> additionalScalars)
    {
        Dictionary<NamedType, IResolvedScalarType> population = new();

        foreach (var keyValue in originalPopulation.Scalars)
        {
            population.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var scalar in additionalScalars)
        {
            population.TryAdd(scalar.Type.AsNamedType(), scalar);
        }

        return new(population);
    }
}
