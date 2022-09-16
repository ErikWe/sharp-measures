namespace SharpMeasures.Generators.Populations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;
using System.Linq;

internal class ExtendedResolvedVectorPopulation : IResolvedVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> Groups => groups;
    public IReadOnlyDictionary<NamedType, IResolvedVectorType> Vectors => vectors;

    private ReadOnlyEquatableDictionary<NamedType, IResolvedVectorGroupType> groups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IResolvedVectorType> vectors { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IResolvedQuantityType)
        .Concat(Vectors.Transform(static (vector) => vector as IResolvedQuantityType)).ToDictionary().AsEquatable();

    private ExtendedResolvedVectorPopulation(IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> groups, IReadOnlyDictionary<NamedType, IResolvedVectorType> vectors)
    {
        this.groups = groups.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();
    }

    public static ExtendedResolvedVectorPopulation Build(IResolvedVectorPopulation originalPopulation, IReadOnlyList<IResolvedVectorGroupType> additionalGroups, IReadOnlyList<IResolvedVectorType> additionalVectors)
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

        foreach (var group in additionalGroups)
        {
            groupPopulation.TryAdd(group.Type.AsNamedType(), group);
        }

        foreach (var vector in additionalVectors)
        {
            vectorPopulation.TryAdd(vector.Type.AsNamedType(), vector);
        }

        return new(groupPopulation, vectorPopulation);
    }
}
