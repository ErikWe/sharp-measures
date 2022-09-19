namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal sealed record class ModifiedUnitValidationContext : IModifiedUnitInstanceValidationContext
{
    public DefinedType Type { get; }

    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName { get; }

    public HashSet<IModifiedUnitInstance> CyclicallyModifiedUnits { get; }

    public ModifiedUnitValidationContext(DefinedType type, IReadOnlyDictionary<string, IUnitInstance> unitInstancesByName, HashSet<IModifiedUnitInstance> cyclicallyModifiedUnits)
    {
        Type = type;

        UnitInstancesByName = unitInstancesByName;

        CyclicallyModifiedUnits = cyclicallyModifiedUnits;
    }
}
