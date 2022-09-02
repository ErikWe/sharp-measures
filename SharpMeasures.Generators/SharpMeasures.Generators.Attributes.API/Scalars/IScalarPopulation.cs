namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IScalarPopulation : IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, IScalarBaseType> ScalarBases { get; }
}
