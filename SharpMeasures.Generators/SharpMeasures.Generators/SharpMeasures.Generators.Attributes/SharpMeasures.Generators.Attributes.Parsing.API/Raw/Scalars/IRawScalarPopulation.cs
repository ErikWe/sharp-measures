namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawScalarPopulation : IRawQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IRawScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawScalarBaseType> ScalarBases { get; }
}
