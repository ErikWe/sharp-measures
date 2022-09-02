namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IQuantityType> Quantities { get; }
    public abstract IReadOnlyDictionary<NamedType, IQuantityBaseType> QuantityBases { get; }
}
