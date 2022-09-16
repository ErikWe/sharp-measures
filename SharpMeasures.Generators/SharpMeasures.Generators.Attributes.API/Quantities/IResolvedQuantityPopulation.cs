namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IResolvedQuantityType> Quantities { get; }
}
