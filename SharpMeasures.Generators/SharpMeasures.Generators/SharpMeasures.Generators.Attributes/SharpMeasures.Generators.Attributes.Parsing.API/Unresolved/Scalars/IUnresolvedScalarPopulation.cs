namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedScalarPopulation : IUnresolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedBaseScalarType> BaseScalarByScalarType { get; }
    public abstract IReadOnlyDictionary<NamedType, UnresolvedSpecializedScalarPopulation> SpecializationsByScalarType { get; }
}

public class UnresolvedSpecializedScalarPopulation : ReadOnlyEquatableDictionary<NamedType, IUnresolvedSpecializedScalarType>
{
    public UnresolvedSpecializedScalarPopulation(IReadOnlyDictionary<NamedType, IUnresolvedSpecializedScalarType> items) : base(items) { }
}
