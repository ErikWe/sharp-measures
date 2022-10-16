namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedVectorPopulation : IResolvedVectorPopulation
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

    private ResolvedVectorPopulation(IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> groups, IReadOnlyDictionary<NamedType, IResolvedVectorType> vectors)
    {
        Groups = groups.AsReadOnlyEquatable();
        Vectors = vectors.AsReadOnlyEquatable();
    }

    public static ResolvedVectorPopulation Build(IReadOnlyList<IResolvedVectorType> vectorBases, IReadOnlyList<IResolvedVectorType> vectorSpecializations, IReadOnlyList<IResolvedVectorGroupType> groupBases, IReadOnlyList<IResolvedVectorGroupType> groupSpecializations, IReadOnlyList<IResolvedVectorType> groupMembers)
    {
        Dictionary<NamedType, IResolvedVectorGroupType> groups = new(groupBases.Count + groupSpecializations.Count);
        Dictionary<NamedType, IResolvedVectorType> vectors = new(vectorBases.Count + vectorSpecializations.Count + groupMembers.Count);

        foreach (var group in groupBases)
        {
            groups.TryAdd(group.Type.AsNamedType(), group);
        }

        foreach (var group in groupSpecializations)
        {
            groups.TryAdd(group.Type.AsNamedType(), group);
        }

        foreach (var vector in vectorBases)
        {
            vectors.TryAdd(vector.Type.AsNamedType(), vector);
        }

        foreach (var vector in vectorSpecializations)
        {
            vectors.TryAdd(vector.Type.AsNamedType(), vector);
        }

        foreach (var member in groupMembers)
        {
            vectors.TryAdd(member.Type.AsNamedType(), member);
        }

        return new(groups, vectors);
    }
}
