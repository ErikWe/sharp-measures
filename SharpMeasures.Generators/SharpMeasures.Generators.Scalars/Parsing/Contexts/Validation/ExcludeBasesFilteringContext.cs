namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public record class ExcludeBasesFilteringContext : IExcludeUnitBasesFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> IncludedUnitInstances { get; }

    public ExcludeBasesFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedUnitInstances)
    {
        Type = type;

        UnitType = unitType;

        IncludedUnitInstances = includedUnitInstances;
    }
}
