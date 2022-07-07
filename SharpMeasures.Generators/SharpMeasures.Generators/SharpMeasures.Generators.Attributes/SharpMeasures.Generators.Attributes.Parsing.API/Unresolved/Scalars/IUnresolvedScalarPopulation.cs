namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

public interface IUnresolvedScalarPopulation
{
    public abstract IScalarPopulation UnresolvedTarget { get; }

    public abstract IReadOnlyDictionary<NamedType, IUnresolvedScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, UnresolvedSpecializedScalarPopulation> Specializations { get; }
}

public class UnresolvedSpecializedScalarPopulation : ReadOnlyEquatableDictionary<NamedType, IUnresolvedSpecializedScalarType>
{
    public UnresolvedSpecializedScalarPopulation(IReadOnlyDictionary<NamedType, IUnresolvedSpecializedScalarType> items) : base(items) { }
}
