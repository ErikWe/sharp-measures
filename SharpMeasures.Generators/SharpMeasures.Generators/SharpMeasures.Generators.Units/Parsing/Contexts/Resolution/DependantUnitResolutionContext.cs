namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class DependantUnitResolutionContext : SimpleProcessingContext, IDependantUnitResolutionContext
{
    public IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByName { get; }

    public DependantUnitResolutionContext(DefinedType type, IReadOnlyDictionary<string, IUnresolvedUnitInstance> unitsByName) : base(type)
    {
        UnitsByName = unitsByName;
    }
}
