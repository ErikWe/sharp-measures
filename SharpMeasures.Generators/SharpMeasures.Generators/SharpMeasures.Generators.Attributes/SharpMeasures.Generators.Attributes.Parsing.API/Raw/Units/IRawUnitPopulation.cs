namespace SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;

public interface IRawUnitPopulation
{
    public IReadOnlyDictionary<NamedType, IRawUnitType> Units { get; }
}
