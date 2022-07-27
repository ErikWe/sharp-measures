namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedScalarPopulation : IUnresolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedScalarBaseType> ScalarBases { get; }
}
