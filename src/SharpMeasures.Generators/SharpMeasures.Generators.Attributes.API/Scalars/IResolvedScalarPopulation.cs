namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedScalarPopulation : IResolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IResolvedScalarType> Scalars { get; }
}
