namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal record class IncludeBasesFilteringContext : IIncludeBasesFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public bool AllBasesIncluded { get; }
    public HashSet<string> IncludedBases { get; }

    public IncludeBasesFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedBases)
    {
        Type = type;

        UnitType = unitType;

        AllBasesIncluded = unitType.UnitsByName.Count == includedBases.Count;
        IncludedBases = includedBases;
    }
}
