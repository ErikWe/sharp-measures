namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class UnitPopulation : ReadOnlyEquatableDictionary<NamedType, UnitInterface>
{
    public UnitPopulation(IReadOnlyDictionary<NamedType, UnitInterface> population) : base(population) { }
}
