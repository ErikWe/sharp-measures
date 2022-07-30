namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;

internal interface IUnresolvedUnitPopulationWithData : IUnresolvedUnitPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedUnitType> DuplicatelyDefined { get; }
}
