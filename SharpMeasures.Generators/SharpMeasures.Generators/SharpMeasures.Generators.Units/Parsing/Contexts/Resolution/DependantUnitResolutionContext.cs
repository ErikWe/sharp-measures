namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

internal record class DependantUnitResolutionContext : SimpleProcessingContext, IDependantUnitResolutionContext
{
    public IReadOnlyDictionary<string, IRawUnitInstance> UnitsByName { get; }

    public DependantUnitResolutionContext(DefinedType type, IReadOnlyDictionary<string, IRawUnitInstance> unitsByName) : base(type)
    {
        UnitsByName = unitsByName;
    }
}
