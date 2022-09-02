namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedVectorPopulation : IResolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IResolvedVectorGroupType> Groups { get; }
    public abstract IReadOnlyDictionary<NamedType, IResolvedVectorType> Vectors { get; }
}
