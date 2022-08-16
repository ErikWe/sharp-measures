namespace SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IRawQuantityType> Quantities { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawQuantityBaseType> QuantityBases { get; }
}
