namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedVectorPopulation : IUnresolvedQuantityPopulation
{
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorType> Vectors { get; }
    public IReadOnlyDictionary<NamedType, UnresolvedSpecializedVectorPopulation> Specializations { get; }
    public IReadOnlyDictionary<NamedType, UnresolvedVectorGroupMembers> Groups { get; }
}

public class UnresolvedSpecializedVectorPopulation : ReadOnlyEquatableDictionary<NamedType, IUnresolvedSpecializedVectorType>
{
    public UnresolvedSpecializedVectorPopulation(IReadOnlyDictionary<NamedType, IUnresolvedSpecializedVectorType> items) : base(items) { }
}

public class UnresolvedVectorGroupMembers : ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorType>
{
    public UnresolvedVectorGroupMembers(IReadOnlyDictionary<NamedType, IUnresolvedVectorType> items) : base(items) { }
}
