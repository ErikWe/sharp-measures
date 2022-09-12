namespace SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IUnitPopulation
{
    public IReadOnlyDictionary<NamedType, IUnitType> Units { get; }
}
