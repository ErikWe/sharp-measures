namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Linq;

internal class ResolvedVectorPopulation : IResolvedVectorPopulation
{
    public static ResolvedVectorPopulation Build(IReadOnlyList<IResolvedVectorType> vectorBases, IReadOnlyList<IResolvedVectorType> vectorSpecializations, IReadOnlyList<IResolvedVectorGroupType> groupBases,
        IReadOnlyList<IResolvedVectorGroupType> groupSpecializations, IReadOnlyList<IResolvedVectorType> groupMembers)
    {
        return new(groupBases.Concat(groupSpecializations).ToDictionary(static (vector) => vector.Type.AsNamedType()), vectorBases.Concat(vectorSpecializations).Concat(groupMembers).ToDictionary(static (vector) => vector.Type.AsNamedType()));
    }

    public IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> Groups => groups;
    public IReadOnlyDictionary<NamedType, IResolvedVectorType> Vectors => vectors;

    private ReadOnlyEquatableDictionary<NamedType, IResolvedVectorGroupType> groups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IResolvedVectorType> vectors { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IResolvedQuantityType)
        .Concat(Vectors.Transform(static (vector) => vector as IResolvedQuantityType)).ToDictionary().AsEquatable();

    private ResolvedVectorPopulation(IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> groups, IReadOnlyDictionary<NamedType, IResolvedVectorType> vectors)
    {
        this.groups = groups.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();
    }
}
