namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Linq;

internal sealed record class ExtendedResolvedVectorPopulation : IResolvedVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> Groups { get; }
    public IReadOnlyDictionary<NamedType, IResolvedVectorType> Vectors { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IResolvedQuantityType) .Concat(Vectors.Transform(static (vector) => vector as IResolvedQuantityType)).ToDictionary().AsEquatable();

    private ExtendedResolvedVectorPopulation(IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> groups, IReadOnlyDictionary<NamedType, IResolvedVectorType> vectors)
    {
        Groups = groups.AsReadOnlyEquatable();
        Vectors = vectors.AsReadOnlyEquatable();
    }

    public static ExtendedResolvedVectorPopulation Build(IResolvedVectorPopulation originalPopulation, ForeignVectorResolutionResult resolutionResult)
    {
        Dictionary<NamedType, IResolvedVectorGroupType> groupPopulation = new();
        Dictionary<NamedType, IResolvedVectorType> vectorPopulation = new();

        foreach (var keyValue in originalPopulation.Groups)
        {
            groupPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in originalPopulation.Vectors)
        {
            vectorPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var group in resolutionResult.Groups)
        {
            groupPopulation.TryAdd(group.Type.AsNamedType(), group);
        }

        foreach (var vector in resolutionResult.Vectors)
        {
            vectorPopulation.TryAdd(vector.Type.AsNamedType(), vector);
        }

        return new(groupPopulation, vectorPopulation);
    }
}
