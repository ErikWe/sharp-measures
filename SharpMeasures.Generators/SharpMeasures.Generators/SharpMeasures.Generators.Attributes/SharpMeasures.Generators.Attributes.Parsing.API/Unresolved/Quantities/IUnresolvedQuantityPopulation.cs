namespace SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> Quantities { get; }
}
