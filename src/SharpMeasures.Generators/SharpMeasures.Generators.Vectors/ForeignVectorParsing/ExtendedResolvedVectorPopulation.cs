namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ExtendedResolvedVectorPopulation : IResolvedVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> Groups { get; }
    public IReadOnlyDictionary<NamedType, IResolvedVectorType> Vectors { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities
    {
        get
        {
            Dictionary<NamedType, IResolvedQuantityType> quantities = new(Groups.Count + Vectors.Count);

            foreach (var group in Groups)
            {
                quantities.Add(group.Key, group.Value);
            }

            foreach (var vector in Vectors)
            {
                quantities.TryAdd(vector.Key, vector.Value);
            }

            return quantities;
        }
    }

    private ExtendedResolvedVectorPopulation(IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> groups, IReadOnlyDictionary<NamedType, IResolvedVectorType> vectors)
    {
        Groups = groups.AsReadOnlyEquatable();
        Vectors = vectors.AsReadOnlyEquatable();
    }

    public static ExtendedResolvedVectorPopulation Build(IResolvedVectorPopulation originalPopulation, ForeignVectorResolutionResult foreignPopulation)
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

        foreach (var group in foreignPopulation.Groups)
        {
            groupPopulation.TryAdd(group.Type.AsNamedType(), group);
        }

        foreach (var vector in foreignPopulation.Vectors)
        {
            vectorPopulation.TryAdd(vector.Type.AsNamedType(), vector);
        }

        return new(groupPopulation, vectorPopulation);
    }
}
