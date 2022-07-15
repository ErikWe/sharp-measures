namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class BiasedUnitResolutionContext : DependantUnitResolutionContext, IBiasedUnitResolutionContext
{
    public bool UnitIncludesBiasTerm { get; }

    public BiasedUnitResolutionContext(DefinedType type, bool unitIncludesBiasTerm, IReadOnlyDictionary<string, IUnresolvedUnitInstance> unitsByName) : base(type, unitsByName)
    {
        UnitIncludesBiasTerm = unitIncludesBiasTerm;
    }
}
