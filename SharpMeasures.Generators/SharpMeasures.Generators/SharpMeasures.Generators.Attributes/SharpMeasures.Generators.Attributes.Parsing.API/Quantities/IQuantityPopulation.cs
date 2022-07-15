namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IQuantityType> Quantities { get; }
}
