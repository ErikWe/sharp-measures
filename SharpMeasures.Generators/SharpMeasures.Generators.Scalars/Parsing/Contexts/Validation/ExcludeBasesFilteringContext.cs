namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public record class ExcludeBasesFilteringContext : IExcludeBasesFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> IncludedBases { get; }

    public ExcludeBasesFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedBases)
    {
        Type = type;

        UnitType = unitType;

        IncludedBases = includedBases;
    }
}
