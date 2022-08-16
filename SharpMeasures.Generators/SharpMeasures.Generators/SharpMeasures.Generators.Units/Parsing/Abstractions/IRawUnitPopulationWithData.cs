namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;

internal interface IRawUnitPopulationWithData : IRawUnitPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IRawUnitType> DuplicatelyDefinedUnits { get; }
}
