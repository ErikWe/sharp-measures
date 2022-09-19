namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using System.Collections.Generic;

internal sealed record class BiasedUnitInstanceValidationContext : IBiasedUnitInstanceValidationContext
{
    public DefinedType Type { get; }

    public bool UnitIncludesBiasTerm { get; }

    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName { get; }

    public HashSet<IModifiedUnitInstance> CyclicallyModifiedUnits { get; }

    public BiasedUnitInstanceValidationContext(DefinedType type, bool unitIncludesBiasTerm, IReadOnlyDictionary<string, IUnitInstance> unitInstancesByName, HashSet<IModifiedUnitInstance> cyclicallyModifiedUnits)
    {
        Type = type;

        UnitIncludesBiasTerm = unitIncludesBiasTerm;

        UnitInstancesByName = unitInstancesByName;

        CyclicallyModifiedUnits = cyclicallyModifiedUnits;
    }
}
