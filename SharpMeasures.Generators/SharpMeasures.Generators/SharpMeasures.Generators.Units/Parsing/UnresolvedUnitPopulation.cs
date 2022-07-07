namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;

internal class UnresolvedUnitPopulation : ReadOnlyEquatableDictionary<NamedType, IUnresolvedUnitType>, IUnresolvedUnitPopulation
{
    IReadOnlyDictionary<NamedType, IUnresolvedUnitType> IUnresolvedUnitPopulation.Units => this;

    public UnresolvedUnitPopulation(IReadOnlyDictionary<NamedType, IUnresolvedUnitType> items) : base(items) { }
}
