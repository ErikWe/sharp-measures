namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public interface IScalarPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, SpecializedScalarPopulation> Specializations { get; }
}

public class SpecializedScalarPopulation : ReadOnlyEquatableDictionary<NamedType, ISpecializedScalarType>
{
    public SpecializedScalarPopulation(IReadOnlyDictionary<NamedType, ISpecializedScalarType> items) : base(items) { }
}
