namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public interface IVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorType> Vectors { get; }
    public IReadOnlyDictionary<NamedType, SpecializedVectorPopulation> Specializations { get; }
    public IReadOnlyDictionary<NamedType, VectorGroupMembers> Groups { get; }
}

public class SpecializedVectorPopulation : ReadOnlyEquatableDictionary<NamedType, ISpecializedVectorType>
{
    public SpecializedVectorPopulation(IReadOnlyDictionary<NamedType, ISpecializedVectorType> items) : base(items) { }
}

public class VectorGroupMembers : ReadOnlyEquatableDictionary<NamedType, IVectorType>
{
    public VectorGroupMembers(IReadOnlyDictionary<NamedType, IVectorType> items) : base(items) { }
}
