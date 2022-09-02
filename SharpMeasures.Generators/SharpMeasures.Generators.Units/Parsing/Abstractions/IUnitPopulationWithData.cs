namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal interface IUnitPopulationWithData : IUnitPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnitType> DuplicatelyDefinedUnits { get; }
}
