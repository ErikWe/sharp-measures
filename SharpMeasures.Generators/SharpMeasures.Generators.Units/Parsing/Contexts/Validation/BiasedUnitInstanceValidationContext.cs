namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using System.Collections.Generic;

internal record class BiasedUnitInstanceValidationContext : ModifiedUnitValidationContext, IBiasedUnitInstanceValidationContext
{
    public bool UnitIncludesBiasTerm { get; }

    public BiasedUnitInstanceValidationContext(DefinedType type, bool unitIncludesBiasTerm, IReadOnlyDictionary<string, IUnitInstance> unitInstancesByName, HashSet<IModifiedUnitInstance> cyclicallyModifiedUnits) : base(type, unitInstancesByName, cyclicallyModifiedUnits)
    {
        UnitIncludesBiasTerm = unitIncludesBiasTerm;
    }
}
