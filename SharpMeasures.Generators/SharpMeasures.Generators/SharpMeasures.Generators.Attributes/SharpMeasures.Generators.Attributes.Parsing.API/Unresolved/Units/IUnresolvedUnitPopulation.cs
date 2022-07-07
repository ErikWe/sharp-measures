namespace SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;

public interface IUnresolvedUnitPopulation
{
    public IReadOnlyDictionary<NamedType, IUnresolvedUnitType> Units { get; }
}
