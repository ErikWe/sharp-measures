namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal record class IncludeBasesFilteringContext : IIncludeUnitBasesFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public bool AllAllUnitInstancesIncluded { get; }
    public HashSet<string> IncludedUnitInstanceNames { get; }

    public IncludeBasesFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedUnitInstanceNames)
    {
        Type = type;

        UnitType = unitType;

        AllAllUnitInstancesIncluded = unitType.UnitInstancesByName.Count == includedUnitInstanceNames.Count;
        IncludedUnitInstanceNames = includedUnitInstanceNames;
    }
}
