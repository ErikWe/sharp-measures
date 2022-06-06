namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal class UnitPopulation : EquatableDictionary<NamedType, UnitInterface>
{
    public UnitPopulation(IReadOnlyDictionary<NamedType, UnitInterface> population) : base(population) { }
}
