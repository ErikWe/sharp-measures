namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal class UnitPopulation : ReadOnlyEquatableDictionary<NamedType, IUnitType>, IUnitPopulation
{
    IReadOnlyDictionary<NamedType, IUnitType> IUnitPopulation.Units => this;

    public UnitPopulation(IReadOnlyDictionary<NamedType, IUnitType> items) : base(items) { }
}
