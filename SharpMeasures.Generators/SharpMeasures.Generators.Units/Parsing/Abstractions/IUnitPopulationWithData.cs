namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal interface IUnitPopulationWithData : IUnitPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnitType> DuplicatelyDefinedUnits { get; }
}
